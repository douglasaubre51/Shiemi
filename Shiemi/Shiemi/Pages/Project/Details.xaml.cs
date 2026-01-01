using Shiemi.PageModels.Project;

namespace Shiemi.Pages.Project;

public partial class Details : ContentPage
{
    public Details(DetailsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}