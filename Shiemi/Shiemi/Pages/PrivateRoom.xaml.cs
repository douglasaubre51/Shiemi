using Shiemi.PageModels;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class PrivateRoom : ContentPage
{
    public PrivateRoom(PrivateRoomPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var pageModel = BindingContext as PrivateRoomPageModel;

        try
        {

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"PrivateRoom init error: {ex.Message}");
        }
    }
}