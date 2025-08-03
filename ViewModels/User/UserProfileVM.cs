using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Services.Storage;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class UserProfileVM : BaseVM
    {
        // di
        readonly SettingsService _settingsService;

        // binders
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        double phoneNo;
        [ObservableProperty]
        string profilePhoto;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotDeveloper))]
        bool isDeveloper;
        public bool IsNotDeveloper => !IsDeveloper;

        // init view
        public UserProfileVM(SettingsService settingsService)
        {
            // di
            _settingsService = settingsService;

            // bind details on init!
            DetailsModel details = UserStorage.FetchUserDetails();
            Username = details.FirstName + " " + details.LastName;
            Email = details.Email;
            PhoneNo = details.PhoneNo;
            ProfilePhoto = details.ProfilePhoto;

            // bind settings on init!
            IsDeveloper = SettingsStorage.FetchDeveloperMode();
        }

        public async Task DeveloperToggleActive()
        {
            if (IsBusy is true) return;

            IsBusy = true;
            try
            {
                bool result = await _settingsService.UpdateDeveloperModeSetting(IsDeveloper);
                if (result is false)
                {
                    IsDeveloper = IsNotDeveloper;
                    return;
                }

                Debug.WriteLine($"switched to {IsDeveloper}");
                SettingsStorage.StoreDeveloperMode(IsDeveloper);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"userprofile-developer-toggle-active-command: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
