using Shiemi.PageModels.User;

namespace Shiemi.Pages.User;

public partial class ViewMoreProjects : ContentPage
{
	public ViewMoreProjects(ViewMoreProjectsPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}
}