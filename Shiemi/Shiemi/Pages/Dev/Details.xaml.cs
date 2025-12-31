using Shiemi.PageModels.Dev;

namespace Shiemi.Pages.Dev;

public partial class Details : ContentPage
{
    public Details(DetailsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

	protected override void OnAppearing()
	{
		var context = BindingContext as DetailsPageModel;
		context.IsPageLoading = true;
	}

	protected override void OnDisappearing()
	{
		var context = BindingContext as DetailsPageModel;
		context.IsPageExiting = true;

		base.OnDisappearing();
	}
}
