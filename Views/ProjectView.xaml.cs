using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class ProjectView : ContentPage
{
    public ProjectView(ProjectVM projectVM)
    {
        InitializeComponent();
        BindingContext = projectVM;
    }
}