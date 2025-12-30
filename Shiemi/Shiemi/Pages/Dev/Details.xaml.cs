using Shiemi.PageModels.Dev;

namespace Shiemi.Pages.Dev;

public partial class Details : ContentPage
{
    public Details(DetailsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

	protected override void OnDisappearing()
	{
		var context = BindingContext as DetailsPageModel;
		context.Messages.Clear();

		base.OnDisappearing();
	}
}
