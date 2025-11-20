using Shiemi.PageModels;

namespace Shiemi.Pages;

public partial class CreateProject : ContentPage
{
    public CreateProject(CreateProjectPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}