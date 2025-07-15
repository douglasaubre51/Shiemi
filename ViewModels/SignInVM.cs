using CommunityToolkit.Mvvm.Input;
using Shiemi.Dto;
using Shiemi.Helpers;
using Shiemi.Models;
using Shiemi.Services;
using System.Diagnostics;

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
    private readonly StorageService _storageService;

    // temp error message holders
    public string tempEmailMessage;
    public string tempPasswordMessage;

    public SignInVM(SignInValidator signInValidator, UserService userService, StorageService storageService)
    {
        _validator = signInValidator;
        _userService = userService;
        _storageService = storageService;
    }

    [RelayCommand]
    async Task TriggerSignIn()
    {
        if (IsBusy is true) return;

        IsBusy = true;

        try
        {
            SignInDto model = new SignInDto()
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

            // call signin service
            string userId = await _userService.RequestSignIn(model);
            if (userId is null) return;

            // request user details
            Details details = await _userService.RequestUserDetails(userId);

            Debug.WriteLine(details.FirstName);
            // save user details
            _storageService.StoreUserDetails(details);
            _storageService.ViewUserDetails();
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
