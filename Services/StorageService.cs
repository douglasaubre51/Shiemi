using Shiemi.Models;

namespace Shiemi.Services
{
    public class StorageService
    {
        public void StoreUserDetails(Details details)
        {
            IPreferences preferences = Preferences.Default;

            preferences.Set("FirstName", details.FirstName);
            preferences.Set("LastName", details.LastName);
            preferences.Set("Email", details.Email);
            preferences.Set("PhoneNo", details.PhoneNo);
            preferences.Set("ProfilePhoto", details.ProfilePhoto);
        }

        public Details FetchUserDetails()
        {
            Details details = new()
            {
                FirstName = Preferences.Default.Get("FirstName", "..."),
                LastName = Preferences.Default.Get("LastName", "..."),
                Email = Preferences.Default.Get("Email", "..."),
                PhoneNo = Preferences.Default.Get("PhoneNo", 0),
                ProfilePhoto = Preferences.Default.Get("ProfilePhoto", "")
            };

            return details;
        }
    }
}
