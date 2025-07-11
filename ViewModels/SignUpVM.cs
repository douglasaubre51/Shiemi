using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Dto;
using Shiemi.Helpers;
using Shiemi.Models;
using Shiemi.Services;

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

        // DI
        private readonly UserService _userService;
        private readonly SignUpValidator _validator;

        public SignUpVM(UserService userService, SignUpValidator signUpValidator)
        {
            Title = "create new account";

            _userService = userService;
            _validator = signUpValidator;
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
                User user = new User()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    PhoneNo = PhoneNo,
                    ConfirmPassword = ConfirmPassword
                };

                // validate attributes
                SignUpValidatorDto? signUpValidatorDto = await _validator.Validate(user);

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
                bool HasCreatedAccount = await _userService.RequestSignUp(user);
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
