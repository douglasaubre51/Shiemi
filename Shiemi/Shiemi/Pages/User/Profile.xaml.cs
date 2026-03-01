using Shiemi.Dtos;
using Shiemi.PageModels.User;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.ViewModels;

namespace Shiemi.Pages.User;

public partial class Profile : ContentPage
{
    private readonly UserService _userService;
    private readonly FlyoutFooterModel _flyoutFooterModel;

    public Profile(
        ProfilePageModel pageModel,
        UserService userService,
        FlyoutFooterModel flyoutFooterModel
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _userService = userService;
        _flyoutFooterModel = flyoutFooterModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Set TitleBar title this pages title !

        try
        {
            ProfilePageModel? pageModel = BindingContext as ProfilePageModel;

            // Fetch user data using String userId !
            string userId = DataStorage.Get("UserId");
            ProfilePageUserDto? user = await _userService.Get(userId);
            if (user is null) return;

            // Set profile view details
            pageModel!.FirstName = user.FirstName;
            pageModel.LastName = user.LastName;
            pageModel.UserName = user.FirstName + "  " + user.LastName;
            pageModel.Email = user.Email;
            pageModel.UserId = user.UserId;
            pageModel.ProfileURL = user.ProfilePhotoURL;

            // Set Flyout Footer data !
            //_flyoutFooterModel.Profile = user.ProfilePhotoURL;
            //_flyoutFooterModel.Username = user.FirstName + " " + user.LastName;
            //_flyoutFooterModel.EmailId = user.Email;
            //_flyoutFooterModel.Role = user.IsDeveloper is true ? "Developer" : string.Empty;

            // Show Activate Dev card !
            if (user.IsDeveloper is true)
            {
                pageModel.DevModeActive = true;
                return;
            }
            pageModel.DevModeActive = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"OnProfileAppearing error: {ex.Message}");
        }
    }
}