using Shiemi.PageModels.Start;

namespace Shiemi.Pages.Start;

public partial class GotBanned : ContentPage
{
    public GotBanned(GotBannedPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}