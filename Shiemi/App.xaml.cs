using Shiemi.Views;

namespace Shiemi
{
    public partial class App : Application
    {
        public TitleBarWindow _window { get; set; }

        public App(TitleBarWindow window)
        {
            InitializeComponent();
            _window = window;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;

            // specify dimensions and positions
            _window.Width = 1280;
            _window.Height = 700;
            _window.X = (displayInfo.Width / displayInfo.Density - _window.Width) / 2;
            _window.Y = (displayInfo.Height / displayInfo.Density - _window.Height) / 2;

            _window.Page = new AppShell();

            return _window;
        }

        protected override async void OnStart()
        {
            // storage clearing done here!

            // login redirection
            bool isUserLoggedIn = Preferences.Default.ContainsKey("UserId");

            if (!isUserLoggedIn)
                await Shell.Current.GoToAsync(nameof(SignInView), true);
            else
                await Shell.Current.GoToAsync("///UserProfileView", true);

            base.OnStart();
        }
    }
}