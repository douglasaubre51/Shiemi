using Shiemi.Dtos;
using Shiemi.PageModels;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

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
            // fetch user data
            string userId = DataStorage.Get("UserId");
            ProfilePageUserDto? user = await _userService.Get(userId);
            if (user is null)
            {
                Debug.WriteLine("OnProfileAppearing status: empty user!");
                return;
            }

            // set user data on profile
            var pageModel = BindingContext as ProfilePageModel;
            if (pageModel is null)
            {
                Debug.WriteLine("OnProfileAppearing status: binding context is null!");
                return;
            }

            pageModel.FirstName = user.FirstName;
            pageModel.LastName = user.LastName;
            pageModel.UserName = user.FirstName + "  " + user.LastName;
            pageModel.Email = user.Email;
            pageModel.UserId = user.UserId;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"OnProfileAppearing error: {ex.Message}");
        }
        base.OnAppearing();
    }
}