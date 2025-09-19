using Shiemi.PageModels;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class Index : ContentPage
{
    private readonly AuthService _authService;
    private readonly EnvironmentService _envService;

    public Index(
        IndexPageModel pageModel,
        AuthService authService,
        EnvironmentService envService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _authService = authService;
        _envService = envService;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            var uri = new Uri(_envService.GetWAGURILoginUri());
            var status = await Browser.Default
                .OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            Debug.WriteLine($"Browser opened: {status}");

            await _authService.ConnectToWAGURI();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("GetStarted btn error: " + ex.Message);
        }
    }
}