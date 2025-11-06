using Shiemi.Utilities.ServiceProviders;

namespace Shiemi;

public partial class App : Application
{
    public App(IServiceProvider provider)
    {
        InitializeComponent();
        Provider.SetProvider(provider);
    }

    protected override Window CreateWindow(IActivationState? activationState)
        => new Window(new AppShell());
}
