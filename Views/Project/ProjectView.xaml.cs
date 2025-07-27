using Shiemi.ViewModels;
using System.Diagnostics;

namespace Shiemi.Views;

public partial class ProjectView : ContentPage
{
    public ProjectView(ProjectVM projectVM)
    {
        InitializeComponent();
        BindingContext = projectVM;
    }

    // on page load
    protected async override void OnAppearing()
    {
        ProjectVM? projectVM = BindingContext as ProjectVM;
        base.OnAppearing();

        try
        {
            await projectVM.FillProjectCollection();
        }
        catch (Exception e)
        {
            Debug.WriteLine("fillproject collection error!");
        }
    }
}