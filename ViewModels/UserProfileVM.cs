using Shiemi.Models;
using Shiemi.Services;

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

        // di
        readonly StorageService _storageService;

        // init view
        public UserProfileVM(StorageService storageService)
        {
            _storageService = storageService;
            Details details = _storageService.FetchUserDetails();

            // bind details on init!
            username = details.FirstName;
            email = details.Email;
            phoneNo = details.PhoneNo;
            profilePhoto = details.ProfilePhoto;
        }
    }
}
