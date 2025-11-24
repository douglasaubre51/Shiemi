using Shiemi.Utilities.ServiceProviders;

namespace Shiemi;

public partial class App : Application
{
    public App(IServiceProvider provider)
    {
        InitializeComponent();

        // init ServiceProvider utility
        Provider.SetProvider(provider);
    }


    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell())
        {
            // set window launch size
            Width = 1100,
            Height = 600
        };

        // center window launch position
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        window.X = (displayInfo.Width / displayInfo.Density - window.Width);
        window.X = (displayInfo.Height / displayInfo.Density - window.Height);

        return window;
    }
}
