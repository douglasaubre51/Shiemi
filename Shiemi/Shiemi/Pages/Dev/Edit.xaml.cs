using Shiemi.PageModels.Dev;

namespace Shiemi.Pages.Dev;

public partial class Edit : ContentPage
{
	public Edit(EditPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}
}