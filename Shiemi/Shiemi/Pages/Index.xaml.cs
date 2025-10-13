using Shiemi.PageModels;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class Index : ContentPage
{
    private readonly AuthService _authService;
    private readonly UserService _userService;
    private readonly EnvironmentStorage _envStorage;
    private readonly UserStorage _userStorage;

    public Index(
        IndexPageModel pageModel,
        AuthService authService,
        EnvironmentStorage envStorage,
        UserService userService,
        UserStorage userStorage
        )
    {
        InitializeComponent();
        BindingContext = pageModel;

        _authService = authService;
        _envStorage = envStorage;
        _userService = userService;
        _userStorage = userStorage;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var pageModel = BindingContext as IndexPageModel;

        try
        {
            pageModel!.IsBusy = true;

            // check if user logged in!
            if (DataStorage.Get("UserId") is not "")
            {
                // store userId<int>
                string userIdString = DataStorage.Get("UserId");
                var userIdDto = await _userService.GetUserId(
                    userIdString
                );
                _userStorage.UserId = userIdDto!.Id;

                pageModel!.IsBusy = false;

                await Shell.Current.GoToAsync(nameof(Profile));
                return;
            }

            // setup connection to WAGURI
            var clientGuid = Guid.NewGuid().ToString();
            var uri = new Uri(_envStorage.GetWAGURILoginUri() + $"/{clientGuid}");
            // launch browser
            var status = await Browser.Default
                .OpenAsync(uri, BrowserLaunchMode.SystemPreferred);

            await _authService.ConnectToWAGURI(clientGuid);

            while (true)
            {
                // successful login logic
                if (DataStorage.Get("UserId") is not "")
                {
                    // store userId<int>
                    string userIdString = DataStorage.Get("UserId");
                    var userIdDto = await _userService.GetUserId(
                        userIdString
                    );
                    _userStorage.UserId = userIdDto!.Id;

                    IsBusy = false;

                    // navigate to profile view
                    await Shell.Current.GoToAsync("//Profile");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("GetStarted btn error: " + ex.Message);
        }
    }
}