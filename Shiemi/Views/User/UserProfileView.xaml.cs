using Shiemi.ViewModels;
using System.Diagnostics;

namespace Shiemi.Views;

public partial class UserProfileView : ContentPage
{
    public UserProfileView(UserProfileVM viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    // is dev switch toggle event
    private async void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        UserProfileVM context = BindingContext as UserProfileVM;

        if (context.IsBusy)
        {
            return;
        }

        try
        {
            await context.DeveloperToggleActive();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"toggle error: {ex}");
        }

        return;
    }
}