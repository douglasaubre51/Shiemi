using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Services;
using System.ComponentModel.DataAnnotations;
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
        string? emailValidationMessage;
        [ObservableProperty]
        string? phoneNoValidationMessage;
        [ObservableProperty]
        string? passwordValidationMessage;

        // DI
        private readonly SendUserDetailsService _userDetailsService;

        public SignUpVM(SendUserDetailsService userDetailsService)
        {
            Title = "create new account";
            _userDetailsService = userDetailsService;
        }

        // for calling http post service
        [RelayCommand]
        async Task SendUserDetails()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            // clear validation msg labels
            PhoneNoValidationMessage = string.Empty;
            EmailValidationMessage = string.Empty;
            PasswordValidationMessage = string.Empty;

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
                var validationContext = new ValidationContext(user);
                var validationResults = new List<ValidationResult>();
                bool validationResult = Validator.TryValidateObject(
                    user,
                    validationContext,
                    validationResults,
                    true
                    );

                // display email validation message label
                var emailErrorMessage = validationResults
                    .Where(e => e.MemberNames.Contains(nameof(User.Email)))
                    .Select(e => e.ErrorMessage)
                    .ToList();
                foreach (var m in emailErrorMessage)
                {
                    EmailValidationMessage += m + "\n";
                }

                // display phone no validation message label
                var phoneNoErrorMessage = validationResults
                    .Where(e => e.MemberNames.Contains(nameof(User.PhoneNo)))
                    .Select(e => e.ErrorMessage)
                    .ToList();
                foreach (var m in phoneNoErrorMessage)
                {
                    PhoneNoValidationMessage += m + "\n";
                }

                // display check password validation message label
                var passwordErrorMessage = validationResults
                    .Where(e => e.MemberNames.Contains(nameof(User.ConfirmPassword)))
                    .Select(e => e.ErrorMessage)
                    .ToList();
                foreach (var m in passwordErrorMessage)
                {
                    PasswordValidationMessage += m.ToString() + "\n";
                }

                // validation error
                if (validationResult is false)
                {
                    foreach (var m in validationResults)
                    {
                        Debug.WriteLine(m.ToString());
                    }
                    await Shell.Current.DisplayAlertAsync(
                        "validation error",
                        "enter all fields properly!",
                        "ok"
                        );
                    return;
                }

                // successfully validated!
                // post request service
                await _userDetailsService.Send(user);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e}");
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
