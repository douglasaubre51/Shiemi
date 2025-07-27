using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class SignInView : ContentPage
{
	public SignInView(SignInVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}