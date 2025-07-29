using Shiemi.ViewModels.Project;

namespace Shiemi.Views.Project;

public partial class EditProjectView : ContentPage
{
    public EditProjectView(EditProjectVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}