using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using FontAwesome6;
using FontAwesome6.Fonts;
using Westwind.Wpf.Statusbar;
using static System.Net.Mime.MediaTypeNames;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string ActiveIconSet { get; set; } = "Default Xaml Icons";

        public MainWindow()
        {
            InitializeComponent();

            btnToggleIcons.Content = ActiveIconSet;
        }

        private void BtnSuccess_OnClick(object sender, RoutedEventArgs e)
        {
            // use the controls methods
            Statusbar.ShowStatusSuccess("Yay. The operation was successful! ", 3000);

        }

        private void BtnError_OnClick(object sender, RoutedEventArgs e)
        {
            // use the controls methods
            Statusbar.ShowStatusError("Ooops. Something went wrong!", 2000);
        }

        private void BtnWarning_OnClick(object sender, RoutedEventArgs e)
        {
            // You can also use the Statusbar.Status object
            // which is more low level
            Statusbar.Status.ShowStatusWarning("Careful... this might go sideways.", 2000);
        }

        private async void BtnProgress_OnClick(object sender, RoutedEventArgs e)
        {
            Statusbar.Status.ShowStatusProgress("This may take a minute...");

            await Task.Delay(2000);

            Statusbar.Status.ShowStatusProgress("Still working...");

            await Task.Delay(2000);

            Statusbar.Status.ShowStatusProgress("Getting close...");

            await Task.Delay(2000);

            Statusbar.Status.ShowStatusSuccess("Yay. All done with success! ");
        }

        private void BtnRaw_OnClick(object sender, RoutedEventArgs e)
        {
            Statusbar.Status.ShowStatus("Customized output. ", 6000, StatusIcons.Default.SuccessIcon, spin: true);
        }

        private void BtnUpdatePanels_OnClick(object sender, RoutedEventArgs e)
        {
            Statusbar.SetStatusCenter("Center Panel Text");

            var sp = new StackPanel() { Orientation = Orientation.Horizontal };
            sp.Children.Add(new TextBlock() { Text = "Right Panel Text" });
            sp.Children.Add(new System.Windows.Controls.Image
                { Source = StatusIcons.Default.SuccessIcon, Height = 15, Margin = new Thickness(3, 0, 0, 0) });

            Statusbar.SetStatusRight(sp);
        }

        private bool ToggleState = false;

        private void BtnToggleIcons_OnChecked(object sender, RoutedEventArgs e)
        {
            ToggleState = !ToggleState;

            if (ToggleState)
            {
                ActiveIconSet = "Custom Images: Font Awesome 6";

                // You can override the control or StatusbarHelper icons

                // Create a new icon set so we don't overwrite default icons
               
                
                var icons = new StatusIcons();

                // create a custom icon for the error icon from FontAwesome6 icons
                var image = new ImageAwesome()
                {
                    PrimaryColor = Brushes.ForestGreen,
                    Height = 15,
                    Icon = EFontAwesomeIcon.Solid_House
                };
                icons.DefaultIcon = image.Source;

                image = new ImageAwesome()
                {
                    PrimaryColor = Brushes.ForestGreen,
                    Height = 15,
                    Icon = EFontAwesomeIcon.Solid_SquareCheck
                };
                icons.SuccessIcon = image.Source;

                // create a custom icon for the error icon from FontAwesome6 icons
                image = new ImageAwesome()
                {
                    PrimaryColor = Brushes.DarkGoldenrod,
                    Height = 15,
                    Icon = EFontAwesomeIcon.Solid_CircleRadiation
                };
                icons.WarningIcon = image.Source;

                image = new ImageAwesome()
                {
                    PrimaryColor = Brushes.Firebrick,
                    Height = 15,
                    Icon = EFontAwesomeIcon.Solid_CircleExclamation
                };
                icons.ErrorIcon = image.Source;

                image = new ImageAwesome()
                {
                    PrimaryColor = Brushes.SteelBlue,
                    Height = 15,
                    Icon = EFontAwesomeIcon.Solid_Spinner
                };
                icons.ProgressIcon = image.Source;

                Statusbar.Status.StatusIcons = icons;
            }
            else
            {
                ActiveIconSet = "Default Xaml Icons";
                Statusbar.Status.StatusIcons = StatusIcons.Default;
            }

            btnToggleIcons.Content = ActiveIconSet;
            Statusbar.Status.SetStatusIcon();

            // Alternately you can also override the StatusIcon.Default icons 
            // and have them overridden anywhere the default icons are used
            // as a 'global' icon override:
            //
            // StatusbarIcons.DefaultIcon = image.Source;  // overrides anywhere the default is used

        }

    }
}
