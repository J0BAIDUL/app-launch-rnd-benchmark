using Windows.UI.Xaml.Controls;

namespace MyApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            TimeTextBlock.Text = $"App.InitializeComponent() parsed in: {App.StartupTimeMs} ms";
        }
    }
}
