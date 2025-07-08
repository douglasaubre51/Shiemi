using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class SignUpVM : BaseVM
    {
        // form binders
        [ObservableProperty]
        string? firstName;
        [ObservableProperty]
        string? lastName;
        [ObservableProperty]
        string? email;
        [ObservableProperty]
        string? password;

        public SignUpVM()
        {
            Title = "create new account";
        }

        // for calling http post service
        [RelayCommand]
        async Task SendUserDetails()
        {
            User user = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };

            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();

            bool validationResult = Validator.TryValidateObject(
                user,
                validationContext,
                validationResults,
                true
                );

            Debug.WriteLine($"validation result: {validationResult}");

            if (validationResult is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "validation error",
                    "enter all fields",
                    "ok"
                    );

                foreach (var m in validationResults)
                {
                    Debug.WriteLine(m.ToString());
                }

                return;
            }

            Debug.WriteLine("success!");
            return;
        }
    }
}
