using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class SignUpView : ContentPage
{
    public SignUpView(SignUpVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}