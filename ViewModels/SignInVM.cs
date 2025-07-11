using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Helpers;
using Shiemi.Models;
using Shiemi.Services;

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

    // di
    private readonly SignInValidator _validator;
    private readonly UserService _userService;

    // temp error message holders
    public string tempEmailMessage;
    public string tempPasswordMessage;

    public SignInVM(SignInValidator signInValidator, UserService userService)
    {
        _validator = signInValidator;
        _userService = userService;
    }

    [RelayCommand]
    async Task TriggerSignIn()
    {
        if (IsBusy is true) return;

        IsBusy = true;

        try
        {
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

            if (!isValid) return;


            // call signin rest service
            bool signInStatus = await _userService.RequestSignIn(model);

            // signed in!
            Debug.WriteLine("Success is permanent!");

            // failure
            if (!signInStatus) return;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"{e}");
            await Shell.Current.DisplayAlertAsync(
                "signin error!",
                "something went wrong!",
                "try again"
            );
            return;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
