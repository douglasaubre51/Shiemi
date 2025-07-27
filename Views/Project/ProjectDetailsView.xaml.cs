using Shiemi.ViewModels.Project;

namespace Shiemi.Views.Project;

public partial class ProjectDetailsView : ContentPage
{
    public ProjectDetailsView(ProjectDetailsVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}