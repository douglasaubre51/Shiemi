using Shiemi.Models;

namespace Shiemi.Services
{
    public static class StorageService
    {
        public static void StoreUserDetails(Details details)
        {
            IPreferences preferences = Preferences.Default;

            preferences.Set("UserId", details.UserId);
            preferences.Set("FirstName", details.FirstName);
            preferences.Set("LastName", details.LastName);
            preferences.Set("Email", details.Email);
            preferences.Set("PhoneNo", details.PhoneNo);
            preferences.Set("ProfilePhoto", details.ProfilePhoto);
        }

        public static Details FetchUserDetails()
        {
            Details details = new()
            {
                UserId = Preferences.Default.Get("UserId", "..."),
                FirstName = Preferences.Default.Get("FirstName", "..."),
                LastName = Preferences.Default.Get("LastName", "..."),
                Email = Preferences.Default.Get("Email", "..."),
                PhoneNo = Preferences.Default.Get("PhoneNo", 0L),
                ProfilePhoto = Preferences.Default.Get("ProfilePhoto", "")
            };

            return details;
        }

        public static void ClearUserData()
        {
            Preferences.Default.Clear();
        }

        public static string GetUserId()
        {
            return Preferences.Default.Get<String>("UserId", string.Empty);
        }
    }
}
