using Shiemi.Models;
using System.Diagnostics;

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
        }

        public void ViewUserDetails()
        {
            Debug.WriteLine(Preferences.Default.Get("FirstName", "no name"));
            Debug.WriteLine(Preferences.Default.Get("LastName", "no name"));
        }
    }
}
