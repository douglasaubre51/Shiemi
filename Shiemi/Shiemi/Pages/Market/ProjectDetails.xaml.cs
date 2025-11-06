using Shiemi.PageModels;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

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
        Debug.WriteLine($"checking if owner: {context!.Project.Id}");

        if (context!.Project.UserId == UserStorage.UserId)
        {
            Debug.WriteLine($"disabling btn: {context.Project.Id}");
            context.NotOwner = false;
        }

        base.OnAppearing();
    }
}