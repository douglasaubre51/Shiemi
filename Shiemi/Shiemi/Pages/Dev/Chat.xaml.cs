using Shiemi.PageModels.Dev;

namespace Shiemi.Pages.Dev;

public partial class Chat : ContentPage
{
	public Chat(ChatPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}
}