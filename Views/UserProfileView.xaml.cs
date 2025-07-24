using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class UserProfileView : ContentPage
{
    public UserProfileView(UserProfileVM viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}