using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class MarketPlaceView : ContentPage
{
    public MarketPlaceView(MarketPlaceVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as MarketPlaceVM;
        await context.LoadProjectsAsync();
    }
}