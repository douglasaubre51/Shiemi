using Shiemi.Models;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class UserProfileVM : BaseVM
    {
        // binders
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        double phoneNo;
        [ObservableProperty]
        string profilePhoto;

        // init view
        public UserProfileVM()
        {
            Title = "Profile";
            Debug.WriteLine(Title);

            DetailsModel details = StorageService.FetchUserDetails();

            // bind details on init!
            username = details.FirstName + " " + details.LastName;
            email = details.Email;
            phoneNo = details.PhoneNo;
            profilePhoto = details.ProfilePhoto;
        }
    }
}
