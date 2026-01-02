using Shiemi.PageModels.User;

namespace Shiemi.Pages.User;

public partial class Home : ContentPage
{
    public Home(HomePageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        var context = BindingContext as HomePageModel;
        context!.IsPageLoading = true;
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        var context = BindingContext as HomePageModel;
        context!.IsPageExiting = true;
        base.OnDisappearing();
    }
}