using Shiemi.PageModels.Project;

namespace Shiemi.Pages.Project;

public partial class AddDevs : ContentPage
{
    public AddDevs(AddDevsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}