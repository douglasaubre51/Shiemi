using Shiemi.Views;

namespace Shiemi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;

            return new Window(new AppShell())
            {
                Width = 1200,
                Height = 700,
                X = (displayInfo.Width / displayInfo.Density - 1200) / 2,
                Y = (displayInfo.Height / displayInfo.Density - 700) / 2
            };
        }

        protected override async void OnStart()
        {
            bool isUserLoggedIn = Preferences.Default.ContainsKey("UserId");

            if (!isUserLoggedIn)
                await Shell.Current.GoToAsync(nameof(SignInView), true);
            else
                await Shell.Current.GoToAsync("///ProjectView", true);

            base.OnStart();
        }
    }
}