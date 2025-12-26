using System.Diagnostics;
using Shiemi.Dtos;
using Shiemi.PageModels.User;
using Shiemi.Services;
using Shiemi.Storage;

namespace Shiemi.Pages.User;

public partial class Profile : ContentPage
{
    private readonly UserService _userService;

    public Profile(
        ProfilePageModel pageModel,
        UserService userService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        try
        {
            
            ProfilePageModel pageModel = (ProfilePageModel)BindingContext;
            if (pageModel is null)
                return;

            // fetch user data
            string userId = DataStorage.Get("UserId");
            ProfilePageUserDto? user = await _userService.Get(userId);
            if (user is null)
                return;

            pageModel.FirstName = user.FirstName;
            pageModel.LastName = user.LastName;
            pageModel.UserName = user.FirstName + "  " + user.LastName;
            pageModel.Email = user.Email;
            pageModel.UserId = user.UserId;
            pageModel.ProfileURL = user.ProfilePhotoURL;
            Debug.WriteLine("profile url: "+pageModel.ProfileURL);

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
        base.OnAppearing();
    }
}