using Shiemi.PageModels.Start;
using Shiemi.Services;
using Shiemi.Storage;

namespace Shiemi.Pages.Start;

public partial class Index : ContentPage
{
    private readonly AuthService _authService;
    private readonly UserService _userService;
    private readonly EnvironmentStorage _envStorage;

    public Index(
        IndexPageModel pageModel,
        AuthService authService,
        EnvironmentStorage envStorage,
        UserService userService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _authService = authService;
        _envStorage = envStorage;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as IndexPageModel;

        try
        {
            context!.IsBusy = true;

            // check if user logged in!
            if (DataStorage.Get("UserId") is not "")
            {
                string userIdString = DataStorage.Get("UserId");
                var userIdDto = await _userService.GetUserId(
                    userIdString
                );
                if (userIdDto is null)
                {
                    DataStorage.Remove("UserId");
                    return;
                }

                // reroute to profile page
                UserStorage.UserId = userIdDto!.Id;
                await Shell.Current.GoToAsync("//DevHub");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SHIEMI Login Session Failed: {ex.Message}");
            await Shell.Current.DisplayAlertAsync(
                "Login error",
                "Error Logging in to SHIEMI Api",
                "Ok");
        }
        finally
        {
            context.IsBusy = false;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var pageModel = BindingContext as IndexPageModel;
        try
        {
            pageModel!.IsBusy = true;

            // start auth session with WAGURI
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
                    UserStorage.UserId = userIdDto!.Id;

                    // navigate to profile view
                    await Shell.Current.GoToAsync("//Profile");
                    return;
                }

                await Task.Delay(100);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("GetStarted btn error: " + ex.Message);
        }
        finally
        {
            pageModel!.IsBusy = false;
        }
    }
}