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

    // services
    private readonly SignInValidator _validator;

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

        bool isValid = _validator.Validate(model);

        if (!isValid)
        {
            IsBusy = false;
            return;
        }

        Debug.WriteLine("Success is permanent!");
        IsBusy = false;
        return;
    }
}
