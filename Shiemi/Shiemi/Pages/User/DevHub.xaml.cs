using Shiemi.PageModels.User;

namespace Shiemi.Pages.User;

public partial class DevHub : ContentPage
{
    public DevHub(DevHubPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnDisappearing()
    {
        var context = BindingContext as DevHubPageModel;
        context!.IsPageExiting = true;
        base.OnDisappearing();
    }
    protected override void OnAppearing()
    {
        var context = BindingContext as DevHubPageModel;
        context!.IsPageLoading = true;
        base.OnAppearing();
        Console.WriteLine("console says : i got seen!");
    }
}
