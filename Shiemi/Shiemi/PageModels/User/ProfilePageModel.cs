using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.PageModels.User;

public partial class ProfilePageModel : BasePageModel
{
    [ObservableProperty]
    private string profileURL = string.Empty;
    [ObservableProperty]
    private string firstName = string.Empty;
    [ObservableProperty]
    private string lastName = string.Empty;
    [ObservableProperty]
    private string userName = string.Empty;
    [ObservableProperty]
    private string email = string.Empty;
    [ObservableProperty]
    private string userId = string.Empty;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowDevCard_NotJoined))]
    private bool devModeActive;
    public bool ShowDevCard_NotJoined => !DevModeActive;

    [RelayCommand]
    async Task GoToProfileEditPage()
    {
        if (IsBusy is true) return;
        IsBusy = true;

        try
        {
            Models.User currentUser = new()
            {
                FirstName = FirstName,
                LastName = LastName
            };
            await Shell.Current.GoToAsync(
                "EditProfile",
                new Dictionary<string, object>()
                {
                    {"CurrentUser",currentUser}
                }
            );
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"User: ProfileError: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoToDevEditPage()
    {
        if (IsBusy is true)
            return;

        IsBusy = true;

        await Shell.Current.GoToAsync("EditDev");
        IsBusy = false;
    }

    [RelayCommand]
    async Task Logout()
    {
        if (IsBusy is true)
            return;

        IsBusy = true;
        bool answer = await Shell.Current.DisplayAlertAsync(
            "Log out",
            "Are you sure you want to log out?",
            "Yes",
            "No"
        );
        if (answer is false)
        {
            IsBusy = false;
            return;
        }

        try
        {
            DataStorage.Remove("UserId");
            await Shell.Current.GoToAsync("//Index");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ProfilePageModel Logout: error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}