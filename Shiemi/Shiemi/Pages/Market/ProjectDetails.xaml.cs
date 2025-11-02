using Shiemi.PageModels;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class ProjectDetails : ContentPage
{
    private readonly UserStorage _userStorage;

    public ProjectDetails(
        ProjectDetailsPageModel pageModel,
        UserStorage userStorage
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _userStorage = userStorage;
    }

    protected override void OnAppearing()
    {
        var context = BindingContext as ProjectDetailsPageModel;
        Debug.WriteLine($"checking if owner: {context!.Project.Id}");

        if (context!.Project.UserId == _userStorage.UserId)
        {
            Debug.WriteLine($"disabling btn: {context.Project.Id}");
            context.NotOwner = false;
        }

        base.OnAppearing();
    }
}