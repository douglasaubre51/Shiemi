using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class ProjectInfoView : ContentPage
{
    public ProjectInfoView(ProjectInfoVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}