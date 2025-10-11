using Shiemi.PageModels;

namespace Shiemi.Pages;

public partial class Projects : ContentPage
{
    public Projects(ProjectsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}