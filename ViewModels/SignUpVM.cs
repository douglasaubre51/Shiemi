using CommunityToolkit.Mvvm.Input;
using Shiemi.Dto;
using Shiemi.Helpers;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class SignUpVM : BaseVM
    {
        // binders
        // form entries
        [ObservableProperty]
        string? firstName;
        [ObservableProperty]
        string? lastName;
        [ObservableProperty]
        string? email;
        [ObservableProperty]
        string? password;
        [ObservableProperty]
        string? confirmPassword;
        [ObservableProperty]
        string? phoneNo;

        // form validation msg
        [ObservableProperty]
        string? firstNameMessage;
        [ObservableProperty]
        string? lastNameMessage;
        [ObservableProperty]
        string? emailMessage;
        [ObservableProperty]
        string? phoneNoMessage;
        [ObservableProperty]
        string? passwordMessage;
        [ObservableProperty]
        string? checkPasswordMessage;

        // imagebutton
        [ObservableProperty]
        string imageSource = string.Empty;
        [ObservableProperty]
        bool imageBtnVisibility = false;

        // image upload button
        [ObservableProperty]
        bool imageUploadBtnVisibility = true;

        // DI
        private readonly UserService _userService;
        private readonly SignUpValidator _validator;

        public SignUpVM(UserService userService, SignUpValidator signUpValidator)
        {
            Title = "create new account";

            _userService = userService;
            _validator = signUpValidator;
        }

        // image upload
        [RelayCommand]
        async Task TriggerPhotoUpload()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            try
            {
                // load file picker
                var image = await FilePicker.Default.PickAsync(
                    new PickOptions
                    {
                        FileTypes = FilePickerFileType.Images
                    }
                    );
                if (image is null) return;

                // turnoff image upload btn
                if (ImageUploadBtnVisibility is true) ImageUploadBtnVisibility = false;

                // display profile pic
                ImageBtnVisibility = true;
                ImageSource = image.FullPath.ToString();

                // call cloudinary api service
            }
            catch (Exception e)
            {
                Debug.WriteLine($"error triggering upload: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // for calling http post service
        [RelayCommand]
        async Task TriggerSignUp()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            // clear validation labels
            FirstNameMessage = string.Empty;
            LastNameMessage = string.Empty;
            EmailMessage = string.Empty;
            PhoneNoMessage = string.Empty;
            PasswordMessage = string.Empty;
            CheckPasswordMessage = string.Empty;

            try
            {
                SignUpDto signUpDto = new()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    PhoneNo = PhoneNo,
                    ConfirmPassword = ConfirmPassword
                };

                // validate attributes
                SignUpValidatorDto? signUpValidatorDto = await _validator.Validate(signUpDto);

                if (signUpValidatorDto is not null)
                {
                    (
                        FirstNameMessage,
                        LastNameMessage,
                        EmailMessage,
                        PhoneNoMessage,
                        PasswordMessage,
                        CheckPasswordMessage
                        ) = signUpValidatorDto;

                    return;
                }

                // signup!
                bool HasCreatedAccount = await _userService.RequestSignUp(signUpDto);
                if (!HasCreatedAccount) return;

                //success
                await Shell.Current.DisplayAlertAsync(
                    "signup successfull!",
                    "created new account successfully!",
                    "continue"
                );
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlertAsync(
                    "signup in error",
                    "error creating account!\ntry again later",
                    "ok"
                    );
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
