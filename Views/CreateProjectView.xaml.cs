using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class CreateProjectView : ContentPage
{
    public CreateProjectView(CreateProjectVM createProjectVM)
    {
        InitializeComponent();
        BindingContext = createProjectVM;
    }
}