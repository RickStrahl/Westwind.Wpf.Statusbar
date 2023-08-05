using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Westwind.Wpf.Statusbar
{
    /// <summary>
    /// Interaction logic for StatusBarControl.xaml
    /// </summary>
    public partial class StatusbarControl : UserControl
    {
        public StatusBarHelper Status { get;  }

        public StatusbarControl()
        {
            InitializeComponent();

            Status = new StatusBarHelper(StatusText, StatusIcon);
        }

        /// <summary>
        /// Shows a success message with a green check icon for the timeout
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="timeout">optional timeout. -1 means use the default icon</param>
        /// <param name="imageSource">Optional imageSource. Defaults to checkbox circle</param>
        /// <param name="flashIcon">if true flashes the icon by briefly making it larger</param>
        public void ShowStatusSuccess(string message, int timeout = -1, ImageSource imageSource = null,
            bool flashIcon = true)
        {
            Status.ShowStatusSuccess(message, timeout, imageSource, flashIcon);
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
            Status.ShowStatusError(message, timeout, imageSource, flashIcon);
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
            Status.ShowStatusWarning(message, timeout, imageSource, flashIcon);
        }

        /// <summary>
        /// Displays an Progress message using common defaults including a spinning icon
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="timeout">optional timeout. -1 means don't time out</param>
        /// <param name="imageSource">Optional imageSource. Defaults to spinning circle</param>
        /// <param name="spin">Determines whether the icons should spin (true by default)</param>
        public void ShowStatusProgress(string message, int timeout = -1, ImageSource imageSource = null,
            bool spin = true, bool flashIcon = false)
        {
            Status.ShowStatusProgress(message, timeout, imageSource, spin, flashIcon);
        }



        public void SetStatusCenter(string text)
        {
            StatusCenter.Content = text;
        }
        public void SetStatusCenter(FrameworkElement control)
        {
            StatusCenter.Content = control;
        }

        public void SetStatusRight(string text)
        {
            StatusRight.Content = text;
        }

        public void SetStatusRight(FrameworkElement control)
        {
            StatusRight.Content = control;
        }
    }
}
