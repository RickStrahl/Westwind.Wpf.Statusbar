using System;
using System.Windows;
using System.Windows.Media;

namespace Westwind.Wpf.Statusbar
{

    /// <summary>
    /// Class that holds the default icons used for status messages
    /// retrieved from internal Geometry resources.
    /// </summary>
    public class StatusIcons
    {
        /// <summary>
        /// Default icons used for status messages
        ///
        /// Default icon are pulled from the following resource dictionary as DrawingImage:
        /// 
        /// "pack://application:,,,/Westwind.Wpf.Statusbar;component/Assets/icons.xaml"
        /// </summary>
        public StatusIcons()
        {
            var dict = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Westwind.Wpf.Statusbar;component/Assets/icons.xaml")
            };
            if (!Application.Current.Resources.Contains("circle_greenDrawingImage"))
                Application.Current.Resources.MergedDictionaries.Add(dict);

            DefaultIcon = dict["circle_greenDrawingImage"] as DrawingImage;
            SuccessIcon = dict["circle_checkDrawingImage"] as DrawingImage;
            ErrorIcon = dict["circle_exclamationDrawingImage"] as DrawingImage;
            WarningIcon = dict["triangle_exclamationDrawingImage"] as DrawingImage;
            ProgressIcon = dict["circle_notchDrawingImage"] as DrawingImage;
        }

        static StatusIcons()
        {
            Default = new StatusIcons();
        }

        /// <summary>
        /// Default icons used for status messages
        /// </summary>
        public static StatusIcons Default { get; set; }

        /// <summary>
        /// The default icon that is displayed in Ready state
        /// </summary>
        public ImageSource DefaultIcon { get; set; }

        /// <summary>
        /// Success icon - circle with checkmark
        /// </summary>
        public ImageSource SuccessIcon { get; set; }

        /// <summary>
        /// Error icon - red triangle with exclamation
        /// </summary>
        public ImageSource ErrorIcon { get; set; }

        /// <summary>
        /// Warning icon - orange triangle with exclamation
        /// </summary>
        public ImageSource WarningIcon { get; set; }

        /// <summary>
        /// Progress icon - notched circle
        /// </summary>
        public ImageSource ProgressIcon { get; set; }
    }
}