using Shiemi.Dtos.ProfileDtos;
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
        base.OnAppearing();

        try
        {
            // load user data
            string userId = DataStorage.Get("UserId");
            Debug.WriteLine($"UserId: {userId}");
            User? user = await _userService.Get(userId);
            if (user is null)
            {
                Debug.WriteLine("OnProfileAppearing status: empty user!");
                return;
            }
            // init user data
            var pageModel = BindingContext as ProfilePageModel;
            pageModel!.FirstName = user.FirstName;
            pageModel!.LastName = user.LastName;
            pageModel!.Email = user.Email;
            pageModel!.UserId = user.Id;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"OnProfileAppearing error: {ex.Message}");
        }
    }
}