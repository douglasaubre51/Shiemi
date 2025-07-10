using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Helpers;
using Shiemi.Models;

namespace Shiemi.ViewModels;

public partial class SignInVM : BaseVM
{
    // form binders
    [ObservableProperty]
    string email;
    [ObservableProperty]
    string password;
    // form validation binders
    [ObservableProperty]
    string emailValidationMessage;
    [ObservableProperty]
    string passwordValidationMessage;

    // services
    private readonly SignInValidator _validator;

    // temp error message holders
    public string tempEmailMessage;
    public string tempPasswordMessage;

    public SignInVM(SignInValidator signInValidator)
    {
        _validator = signInValidator;
    }

    [RelayCommand]
    async Task TriggerSignIn()
    {
        if (IsBusy is true) return;

        IsBusy = true;

        SignIn model = new SignIn()
        {
            Email = Email,
            Password = Password
        };

        // call helper
        bool isValid = _validator.Validate(model, out tempEmailMessage, out tempPasswordMessage);

        // send error messages to error label binders
        EmailValidationMessage = tempEmailMessage;
        PasswordValidationMessage = tempPasswordMessage;

        if (!isValid)
        {
            IsBusy = false;
            return;
        }

        // validation successful!
        Debug.WriteLine("Success is permanent!");
        IsBusy = false;
        return;
    }
}
