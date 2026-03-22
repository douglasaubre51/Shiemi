using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Storage;

namespace Shiemi.PageModels.User;

[QueryProperty("CurrentUser", "CurrentUser")]
public partial class EditPageModel(
    UserService userServ) : BasePageModel
{
    [ObservableProperty]
    private OptionalUserDetails? _optionalUserDetails = new();

    [ObservableProperty]
    private Models.User? currentUser;

    [ObservableProperty]
    private string firstName = string.Empty;
    [ObservableProperty]
    private string lastName = string.Empty;
    [ObservableProperty]
    private FileResult? profilePhoto;

    [ObservableProperty]
    private ProgressBar? customProgressBar;

    private readonly UserService _userServ = userServ;


    [RelayCommand]
    async Task ChooseProfilePhoto()
    {
        try
        {
            ProfilePhoto = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select a Photo",
                FileTypes = FilePickerFileType.Images
            });
            if (ProfilePhoto is null)
            {
                Debug.WriteLine("failed to choose photo!");
                IsBusy = false;
                return;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    async Task GoBack()
        => await Shell.Current.GoToAsync("..");

    async Task Loader()
    {
        while (true)
        {
            if (IsBusy is false)
                return;

            await CustomProgressBar!.ProgressTo(0, 1, Easing.SpringIn);
            await CustomProgressBar!.ProgressTo(.25, 500, Easing.SpringIn);
            await CustomProgressBar!.ProgressTo(.50, 500, Easing.SpringIn);
            await CustomProgressBar!.ProgressTo(.75, 500, Easing.SpringIn);
            await CustomProgressBar!.ProgressTo(1, 500, Easing.SpringIn);

            await Task.Delay(500);
        }
    }

    [RelayCommand]
    async Task EditProfile()
    {
        if (IsBusy is true) return;
        IsBusy = true;

        try
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                IsBusy = false;
                return;
            }
            CustomProgressBar!.Progress = 0;
            _ = Loader();

            Models.User user = new()
            {
                Id = UserStorage.UserId,
                FirstName = FirstName,
                LastName = LastName,
            };

            // Update User Details!
            bool userDetailsUploadResponse = await _userServ.UpdateUserDetails(
                user,
                OptionalUserDetails!
                );
            if (userDetailsUploadResponse is false)
            {
                IsBusy = false;
                await Shell.Current.DisplayAlertAsync(
                    "Update Error",
                    "Couldnot update user account!",
                    "Ok");

                return;
            }

            // Update Profile Photo!
            bool userProfilePhotoUploadResponse = false;
            if (ProfilePhoto is not null)
            {
                userProfilePhotoUploadResponse = await _userServ.UpdateUserProfilePhoto(
                    user.Id,
                    ProfilePhoto.FullPath
                    );
                if (userProfilePhotoUploadResponse is false)
                {
                    IsBusy = false;
                    await Shell.Current.DisplayAlertAsync(
                        "Update Error",
                        "Couldnot update user profile Photo!",
                        "Ok");

                    return;
                }
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"User: EditProfileError: {ex.Message}");
            await Shell.Current.DisplayAlertAsync(
                "Edit Profile error",
                "Failed to edit user profile!",
                "Ok"
            );
        }
        finally
        {
            IsBusy = false;
        }
    }
}