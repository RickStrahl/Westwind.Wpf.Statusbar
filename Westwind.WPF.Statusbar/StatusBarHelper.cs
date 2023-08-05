﻿using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Westwind.Wpf.Statusbar.Utilities;


namespace Westwind.Wpf.Statusbar
{
    /// <summary>
    /// Statusbar helper method that is passed a textblock and image
    /// to set a status message and control status icon display.
    ///
    /// This class handles displaying of the message, restoring to
    /// the default icon, flashing icons displayed and optionally
    /// controlling the images that are displayed for icons.
    ///
    /// Can be generically applied to any control that has a textblock
    /// and some sort of image control that serves as an icon.
    /// </summary>
    /// <remarks>
    /// By default all status operations are performed using the
    /// WPF Dispatcher to ensure the UI is updated before the
    /// next operation is performed.
    /// </remarks>
    public class StatusBarHelper
    {

        DebounceDispatcher debounce = new DebounceDispatcher();

        /// <summary>
        /// The textbox that holds the main status text  of the status bar
        /// </summary>
        public TextBlock StatusText { get; set; }


        /// <summary>
        /// An Image  control that displays the icon that is displayed.
        /// Different display modes will change the icon.
        ///
        /// Default icons used can be found in `StatusIcons.Default`.
        /// and you can override the Source with any other ImageSource.
        /// </summary>
        public Image StatusImage { get; set; }

        /// <summary>
        /// Default timeout for how long a status message displays
        /// </summary>
        public int StatusMessageTimeoutMs { get; set; } = 6000;

        
        /// <summary>
        /// The Default color that's used when the timeout is up and reverts
        /// to a default state.
        /// </summary>
        public Brush DefaultIconColor { get; set; } = Brushes.LimeGreen;

        /// <summary>
        /// Original Icon Height to reset to. If 0 uses value from XAML
        /// If value is set to auto, 15 is used
        /// </summary>
        public double OriginalIconHeight = 0F;

        /// <summary>
        /// Original Icon Height to reset to. If 0 uses value from XAML
        /// If value is set to auto, 15 is used
        /// </summary>
        public double OriginalIconWidth = 0F;


        /// <summary>
        /// Default Status Icon Images
        /// </summary>
        public StatusIcons StatusIcons { get; set; } 


        public StatusBarHelper(TextBlock statusText, Image statusIconImage)
        {            
            StatusText = statusText;
            StatusImage = statusIconImage;
            StatusIcons = StatusIcons.Default;

            if (!double.IsNaN(OriginalIconHeight) && OriginalIconHeight != 0)
                StatusImage.Height = OriginalIconHeight;
            else
            {
                OriginalIconHeight = StatusImage.Height;
                if (double.IsNaN(OriginalIconHeight) || OriginalIconHeight < 1)
                    StatusImage.Height = 15F;
            }
            if (!double.IsNaN(OriginalIconHeight) && OriginalIconHeight != 0)
                StatusImage.Height = OriginalIconHeight;
            else
            {
                OriginalIconWidth = StatusImage.Width;
                if (double.IsNaN(OriginalIconWidth) || OriginalIconWidth < 1)
                    StatusImage.Width = 15F;
            }
        }

        #region High level Status Operations

        /// <summary>
        /// Shows a success message with a green check icon for the timeout
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="timeout">optional timeout. -1 means use the default icon</param>
        /// <param name="imageSource">Optional imageSource. Defaults to checkbox circle</param>
        /// <param name="flashIcon">if true flashes the icon by briefly making it larger</param>
        public void ShowStatusSuccess(string message, int timeout = -1, ImageSource imageSource = null, bool flashIcon = true)
        {
            if (timeout == -1)
                timeout = StatusMessageTimeoutMs;

            if (imageSource == null)
            {
                imageSource = StatusIcons.SuccessIcon;
            }

            ShowStatus(message, timeout, imageSource, flashIcon: flashIcon);
        }

        /// <summary>
        /// Displays an error message using common defaults for a timeout milliseconds
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="timeout">optional timeout. -1 means use the default icon.</param>
        /// <param name="imageSource">Optional imageSource. Defaults to red error triangle</param>
        /// <param name="flashIcon">if true flashes the icon by briefly making it larger</param>
        public void ShowStatusError(string message, int timeout = -1, 
            ImageSource imageSource = null,
             bool flashIcon = true)
        {
            if (timeout == -1)
                timeout = StatusMessageTimeoutMs;


            if (imageSource == null)
            {
                imageSource = StatusIcons.ErrorIcon;
            }

            ShowStatus(message, timeout, imageSource,  flashIcon: flashIcon);
        }

        /// <summary>
        /// Displays an orange warning message using common defaults for a timeout milliseconds
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="timeout">optional timeout. -1 means revert back to default icon after default timeout</param>
        /// <param name="imageSource">Optional imageSource. Defaults to orange warning triangle</param>
        /// <param name="flashIcon">if true flashes the icon by briefly making it larger</param>
        public void ShowStatusWarning(string message, int timeout = -1,
            ImageSource imageSource = null,
            bool flashIcon = true)
        {
            if (timeout == -1)
                timeout = StatusMessageTimeoutMs;


            if (imageSource == null)
            {
                imageSource = StatusIcons.WarningIcon;
            }

            ShowStatus(message, timeout, imageSource, flashIcon: flashIcon);
        }
        

        /// <summary>
        /// Displays an Progress message using common defaults including a spinning icon
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="timeout">optional timeout. -1 means don't time out</param>
        /// <param name="imageSource">Optional imageSource. Defaults to spinning circle</param>
        /// <param name="spin">Determines whether the icons should spin (true by default)</param>
        public void ShowStatusProgress(string message, int timeout = -1, ImageSource imageSource = null, bool spin = true, bool flashIcon = false)
        {
            if (timeout == -1)
                timeout = 0; // don't timeout

            if (imageSource == null)
            {
                imageSource = StatusIcons.ProgressIcon;
            }

            ShowStatus(message, timeout, imageSource, spin: spin, flashIcon: flashIcon);
        }

        /// <summary>
        /// Status the statusbar icon on the left bottom to some indicator
        /// </summary>
        /// <param name="imageSource">Allows you to explcitly set the icon's imageSource</param>
        /// <param name="spin">Optionally spin the icon</param>
        public void SetStatusIcon(ImageSource imageSource, bool spin = false)
        {
            StatusImage.Source = imageSource;

            if (spin)
            {
                var animation = new DoubleAnimation(0, 360, TimeSpan.FromMilliseconds(2000));
                animation.RepeatBehavior = RepeatBehavior.Forever;

                var rotateTransform = new RotateTransform();
                rotateTransform.Angle = 360;
                rotateTransform.CenterX = StatusImage.Width / 2;
                rotateTransform.CenterY = StatusImage.Height /2 ;

                StatusImage.RenderTransform = rotateTransform;
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
            else
            {
                StatusImage.RenderTransform = null;
            }
        }

        #endregion

        #region Low Level Status Operations

        /// <summary>
        /// Low level ShowStatus method that handles all status operations
        /// and that is called from the higher level ShowStatusXXX methods
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="timeoutMs">Timeout until returning to default icon.
        ///  0 -  means icon does not revert to default
        /// </param>
        /// 
        /// <param name="imageSource">Image source to render. You can use `StatusIcons.Default` for the default icons</param>
        /// <param name="spin">Spin icon</param>
        /// <param name="flashIcon">if true flashes the icon by briefly making it larger</param>
        /// <param name="noDispatcher">Status update occurs outside of the Dispatcher</param>
        public void ShowStatus(string message = null, 
            int timeoutMs = 0,
            ImageSource imageSource = null,
            bool spin = false, 
            bool flashIcon = false, 
            bool noDispatcher = false)
        {
            // check for disabled dispatcher which will throw exceptions
            if (!noDispatcher) // && !WindowUtilities.IsDispatcherDisabled())
                // run in a dispatcher here to force the UI to be updated before render cycle
                Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
                    ShowStatusInternal(message, timeoutMs, imageSource, spin, flashIcon: flashIcon)));
            else
                // dispatcher blocked - just assign and let Render handle
                ShowStatusInternal(message, timeoutMs, imageSource, spin, flashIcon: flashIcon);
        }


        private void ShowStatusInternal(string message = null, 
            int milliSeconds = 0,
            ImageSource imageSource = null,
           
            bool spin = false, bool noDispatcher = false, 
            bool flashIcon = false)
        {

            if (imageSource == null)
            {
                imageSource = StatusIcons.DefaultIcon;
            }
            SetStatusIcon(imageSource, spin);

            if (message == null)
            {
                message = "Ready";
                SetStatusIcon();
            }

            StatusText.Text = message;

            if (milliSeconds > 0)
            {
                // debounce rather than delay so if something else displays
                // a message the delay timer is 'reset'
                debounce.Debounce(milliSeconds, (p) => ShowStatus(null, 0), null);
            }

            if (flashIcon)
                FlashIcon();
        }

        #endregion


        #region Helpers

        /// <summary>
        /// Resets the status icon to the default icon
        /// </summary>
        public void SetStatusIcon()
        {
            StatusImage.RenderTransform = null;
            StatusImage.Source = StatusIcons.DefaultIcon;
        }

        /// <summary>
        /// Flashes the icon briefly by making it larger then reverting back to its original size
        /// </summary>
        /// <param name="icon">Optionally pass an Image control. Defaults to the Icon Image control</param>
        public void FlashIcon(Image icon = null)
        {
            if (icon == null)
                icon = StatusImage;

            var origSize = OriginalIconHeight;
            DoubleAnimation animation = new DoubleAnimation(origSize* 1.5, TimeSpan.FromMilliseconds(700));
            
            void OnAnimationOnCompleted(object s, EventArgs e)
            {
                var animation2 = new DoubleAnimation(origSize, TimeSpan.FromMilliseconds(500));
                icon.BeginAnimation(Image.WidthProperty, animation2);
                icon.BeginAnimation(Image.HeightProperty, animation2);

                animation.Completed -= OnAnimationOnCompleted;
            }
            animation.Completed += OnAnimationOnCompleted;

            icon.BeginAnimation(Image.WidthProperty, animation);
            icon.BeginAnimation(Image.HeightProperty, animation);
        }

        #endregion
    }
}