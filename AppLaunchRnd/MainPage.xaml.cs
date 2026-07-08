using Windows.UI.Xaml.Controls;

namespace MyApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Windows.UI.Xaml.Window.Current.SetTitleBar(AppTitleBar);
            InitTimeText.Text = $"InitializeComponent(): {App.InitComponentMs} ms";
            LaunchTimeText.Text = $"Ctor → OnLaunched(): {App.CtorToLaunchedMs} ms";
            TotalTimeText.Text = $"Total: {App.InitComponentMs + App.CtorToLaunchedMs} ms";
        }
    }
}
