using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MyApp
{
    sealed partial class App : Application
    {
        public static long InitComponentMs;
        public static long CtorToLaunchedMs;

        private static System.Diagnostics.Stopwatch _ctorToLaunchTimer;

        public App()
        {
            var initTimer = System.Diagnostics.Stopwatch.StartNew();
            this.InitializeComponent();
            initTimer.Stop();
            InitComponentMs = initTimer.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"[BENCHMARK] InitializeComponent() took: {InitComponentMs} ms");

            _ctorToLaunchTimer = System.Diagnostics.Stopwatch.StartNew();

            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            _ctorToLaunchTimer.Stop();
            CtorToLaunchedMs = _ctorToLaunchTimer.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"[BENCHMARK] Ctor->OnLaunched() took: {CtorToLaunchedMs} ms");

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
