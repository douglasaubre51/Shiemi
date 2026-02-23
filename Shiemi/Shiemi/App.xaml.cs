using MvvmHelpers;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;
using Shiemi.Views;

namespace Shiemi;

public partial class App : Application
{
    public App(IServiceProvider provider)
    {
        InitializeComponent();

        // Init ServiceProvider utility
        Provider.SetProvider(provider);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Send flyout footer model to Shell.Flyout !
        var titleBarViewModel = Provider.GetService<BaseViewModel>();
        var flyoutFooterModel = Provider.GetService<FlyoutFooterModel>();
        var window = new Window(new AppShell(flyoutFooterModel!))
        {
            Width = 1300,
            Height = 750,
            TitleBar = new TitleBarView(),
            BindingContext = titleBarViewModel
        };

        // Center window launch position
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        window.X = displayInfo.Width / displayInfo.Density - window.Width;
        window.X = displayInfo.Height / displayInfo.Density - window.Height;

        return window;
    }
}