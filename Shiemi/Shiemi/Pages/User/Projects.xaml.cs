using Shiemi.PageModels;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class Projects : ContentPage
{
    private readonly ProjectService _projectService;

    public Projects(
        ProjectsPageModel pageModel,
        ProjectService projectService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _projectService = projectService;
    }

    protected override async void OnAppearing()
    {
        var pageModel = BindingContext as ProjectsPageModel;
        var collection = pageModel!.ProjectCollection;
        collection.Clear();

        try
        {
            var projectList = await _projectService.GetAllByUser(
                UserStorage.UserId
                );
            foreach (var project in projectList!)
            {
                Debug.WriteLine($"title: {project.Title}");
                collection.Add(project);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Loading UserProjectList error: {ex.Message}");
        }
        finally
        {
            base.OnAppearing();
        }
    }
}