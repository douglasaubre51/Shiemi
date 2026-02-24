using Shiemi.PageModels.Project;

namespace Shiemi.Pages.Project;

public partial class EditProject : ContentPage
{
    public EditProject(EditProjectPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}