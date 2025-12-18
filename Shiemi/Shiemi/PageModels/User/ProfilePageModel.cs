using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Services;
using Shiemi.Storage;

namespace Shiemi.PageModels.User;

public partial class ProfilePageModel(
    DevService devServ
) : BasePageModel
{
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

    private readonly DevService _devServ = devServ;

    [RelayCommand]
    async Task MakeDeveloper()
    {
        if (IsBusy is true)
            return;

        try
        {
            IsBusy = true;
            bool result = await _devServ.SetDeveloper(UserStorage.UserId);
            if (result is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Dev mode error",
                    "Error enabling Dev Mode !",
                    "Ok"
                );
                return;
            }

            DevModeActive = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ProfilePageModel MakeDeveloper: error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task Logout()
    {
        if (IsBusy is true)
            return;

        IsBusy = true;
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