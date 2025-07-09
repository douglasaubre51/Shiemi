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

                // form validation
                var validationContext = new ValidationContext(user);
                var validationResults = new List<ValidationResult>();
                bool validationResult = Validator.TryValidateObject(
                    user,
                    validationContext,
                    validationResults,
                    true
                    );

                // validation error
                if (validationResult is false)
                {
                    foreach (var m in validationResults)
                    {
                        Debug.WriteLine(m.ToString());
                    }
                    await Shell.Current.DisplayAlertAsync(
                        "validation error",
                        "enter all fields",
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
