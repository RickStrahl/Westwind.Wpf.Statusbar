using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Westwind.Wpf.Statusbar;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSuccess_OnClick(object sender, RoutedEventArgs e)
        {
            // use the controls methods
            Statusbar.ShowStatusSuccess("Yay. The operation was successful! ", 3000);

        }

        private void BtnError_OnClick(object sender, RoutedEventArgs e)
        {
            // use the controls methods
            Statusbar.ShowStatusError("Ooops. Something went wrong!",2000);
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
            sp.Children.Add(new Image { Source = StatusIcons.Default.SuccessIcon, Height=15, Margin = new Thickness(3, 0 ,0, 0)});

            Statusbar.SetStatusRight(sp);
        }

    }
}
