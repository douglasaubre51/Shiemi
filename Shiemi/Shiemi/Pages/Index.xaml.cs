using System.Diagnostics;

namespace Shiemi.Pages;

public partial class Index : ContentPage
{
    public Index()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            var uri = new Uri("http://localhost:5020/Auth/Login/002");
            var status = await Browser.Default
                .OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            Debug.WriteLine($"Browser opened: {status}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("GetStarted btn error: " + ex.Message);
        }
    }
}