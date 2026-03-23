using Shiemi.Dtos;
using Shiemi.Models;
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

        try
        {
            ProfilePageModel? pageModel = BindingContext as ProfilePageModel;
            if (pageModel!.IsWatchingProfile is true)
            {
                UserDto? foreignUser = await _userService.GetUserById(pageModel.ForeignUserId);
                if (foreignUser is null)
                {
                    await Shell.Current.GoToAsync("///Profile");
                    return;
                }

                // Set profile view details
                pageModel!.FirstName = foreignUser.FirstName;
                pageModel.LastName = foreignUser.LastName;
                pageModel.UserName = foreignUser.FirstName + "  " + foreignUser.LastName;
                pageModel.Email = foreignUser.Email;
                pageModel.ProfileURL = foreignUser.ProfilePhotoURL;
                pageModel.Id = foreignUser.Id;

                // Set optional user details
                OptionalUserDetails? optionalDetails = await _userService.GetOptionalDetails(pageModel.ForeignUserId);
                pageModel.OptionalUserDetails = optionalDetails;

                UserActionsBtnLayout.IsVisible = false;
                GoBackBtn.IsVisible = true;

                return;
            }

            UserActionsBtnLayout.IsVisible = true;
            GoBackBtn.IsVisible = false;

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
            pageModel.Id = user.Id;

            // Set optional user details
            OptionalUserDetails? optionalUserDetails = await _userService.GetOptionalDetails(user.Id);
            pageModel.OptionalUserDetails = optionalUserDetails;

            // Set Flyout Footer data !
            _flyoutFooterModel.Profile = user.ProfilePhotoURL;
            _flyoutFooterModel.Username = user.FirstName + " " + user.LastName;
            _flyoutFooterModel.EmailId = user.Email;
            _flyoutFooterModel.Role = user.IsDeveloper is true ? "Developer" : string.Empty;

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