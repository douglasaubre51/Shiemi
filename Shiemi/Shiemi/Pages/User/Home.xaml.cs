using Shiemi.PageModels.User;

namespace Shiemi.Pages.User;

public partial class Home : ContentPage
{
    public Home(HomePageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}