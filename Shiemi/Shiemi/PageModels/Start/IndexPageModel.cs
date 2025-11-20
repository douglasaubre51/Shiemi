using CommunityToolkit.Mvvm.Input;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.PageModels
{
    public partial class IndexPageModel : BasePageModel
    {
        public IndexPageModel()
            => Title = "Start";

        [RelayCommand]
        async Task LogoutPressed()
        {
            try
            {
                IsBusy = true;

                Debug.WriteLine("removing UserId ...");
                DataStorage.Remove("UserId");

                IsBusy = false;

                await Shell.Current.DisplayAlertAsync(
                    "Shiemi",
                    "Logged out current user!",
                    "Ok"
                    );
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LogoutError: {ex.Message}");
                IsBusy = false;

                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    "Something went wrong!",
                    "Ok"
                    );
            }
        }
    }
}
