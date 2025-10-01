using Shiemi.PageModels;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class Index : ContentPage
{
    private readonly AuthService _authService;
    private readonly EnvironmentStorage _envStorage;

    public Index(
        IndexPageModel pageModel,
        AuthService authService,
        EnvironmentStorage envStorage
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _authService = authService;
        _envStorage = envStorage;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            // check if user logged in!
            if (DataStorage.Get("UserId") is not "")
            {
                await Shell.Current.GoToAsync(nameof(Profile));
                return;
            }

            // setup connection to WAGURI
            var clientGuid = Guid.NewGuid().ToString();
            var uri = new Uri(_envStorage.GetWAGURILoginUri() + $"/{clientGuid}");
            var status = await Browser.Default
                .OpenAsync(uri, BrowserLaunchMode.SystemPreferred);

            await _authService.ConnectToWAGURI(clientGuid);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("GetStarted btn error: " + ex.Message);
        }
    }
}