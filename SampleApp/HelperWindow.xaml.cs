using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FontAwesome6;
using FontAwesome6.Fonts;
using Westwind.Wpf.Statusbar;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HelperWindow : Window
    {

        public string ActiveIconSet { get; set; } = "Default Xaml Icons";

        public StatusbarHelper Status { get; set; }

        public HelperWindow()
        {
            InitializeComponent();

            Status = new StatusbarHelper(StatusText, StatusIcon);

            btnToggleIcons.Content = ActiveIconSet;
        }

        private void BtnSuccess_OnClick(object sender, RoutedEventArgs e)
        {
            // use the controls methods
            Status.ShowStatusSuccess("Yay. The operation was successful! ", 3000);

        }

        private void BtnError_OnClick(object sender, RoutedEventArgs e)
        {
            // use the controls methods
            Status.ShowStatusError("Ooops. Something went wrong!", 2000);
        }

        private void BtnWarning_OnClick(object sender, RoutedEventArgs e)
        {
            // You can also use the Statusbar.Status object
            // which is more low level
            Status.ShowStatusWarning("Careful... this might go sideways.", 2000);
        }

        private async void BtnProgress_OnClick(object sender, RoutedEventArgs e)
        {
            Status.ShowStatusProgress("This may take a minute...");

            await Task.Delay(2000);

            Status.ShowStatusProgress("Still working...");

            await Task.Delay(2000);

            Status.ShowStatusProgress("Getting close...");

            await Task.Delay(2000);

            Status.ShowStatusSuccess("Yay. All done with success! ");
        }

        private async void BtnRaw_OnClick(object sender, RoutedEventArgs e)
        {

            var image = new ImageAwesome()
            {
                PrimaryColor = Brushes.SteelBlue,
                Height = 15,
                Icon = EFontAwesomeIcon.Solid_Spinner
            };
            Status.ShowStatusProgress("Custom icon spinner (from FontAwesome)",imageSource: image.Source, spin: true);

            await Task.Delay(3000);

            Status.ShowStatus("Using a different stock icon customized output. ", 3000, StatusIcons.Default.SuccessIcon, spin: true);
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
                    PrimaryColor = Brushes.Green,
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

                Status.StatusIcons = icons;
            }
            else
            {
                ActiveIconSet = "Default Xaml Icons";
                Status.StatusIcons = StatusIcons.Default;
            }

            btnToggleIcons.Content = ActiveIconSet;
            Status.SetStatusIcon();

            // Alternately you can also override the StatusIcon.Default icons 
            // and have them overridden anywhere the default icons are used
            // as a 'global' icon override:
            //
            // StatusbarIcons.DefaultIcon = image.Source;  // overrides anywhere the default is used

        }


    }
}
