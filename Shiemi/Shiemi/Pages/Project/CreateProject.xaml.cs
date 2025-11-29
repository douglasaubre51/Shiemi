using Shiemi.PageModels.Project;

namespace Shiemi.Pages.Project;

public partial class CreateProject : ContentPage
{
    public CreateProject(CreateProjectPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}