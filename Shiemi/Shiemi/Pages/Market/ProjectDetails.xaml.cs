using System.Diagnostics;
using Shiemi.PageModels.Market;
using Shiemi.Storage;

namespace Shiemi.Pages.Market;

public partial class ProjectDetails : ContentPage
{
    public ProjectDetails(ProjectDetailsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        var context = BindingContext as ProjectDetailsPageModel;
        Debug.WriteLine($"checking if owner: {context!.ProjectVM.Id}");

        if (context!.ProjectVM.UserId == UserStorage.UserId)
        {
            Debug.WriteLine($"disabling btn: {context.ProjectVM.Id}");
            context.NotOwner = false;
        }

        base.OnAppearing();
    }
}