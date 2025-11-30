using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.PageModels.User;

public partial class ProfilePageModel : BasePageModel
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

    [RelayCommand]
    async Task Logout()
    {
        if (IsBusy is true)
            return;

        IsBusy = true;

        try
        {
            // clear userid string
            DataStorage.Remove("UserId");

            // go to auth page!
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
