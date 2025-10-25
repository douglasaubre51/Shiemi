using Shiemi.PageModels;

namespace Shiemi.Pages;

public partial class ProjectDetails : ContentPage
{
    public ProjectDetails(ProjectDetailsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}