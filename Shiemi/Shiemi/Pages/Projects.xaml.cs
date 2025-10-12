using Shiemi.PageModels;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class Projects : ContentPage
{
    private readonly ProjectService _projectService;
    private readonly UserStorage _userStorage;

    public Projects(
        ProjectsPageModel pageModel,
        ProjectService projectService,
        UserStorage userStorage
        )
    {
        InitializeComponent();
        BindingContext = pageModel;

        _projectService = projectService;
        _userStorage = userStorage;
    }

    protected override async void OnAppearing()
    {
        var pageModel = BindingContext as ProjectsPageModel;
        var collection = pageModel!.ProjectCollection;
        collection.Clear();

        try
        {
            var projectList = await _projectService.GetAll(
                _userStorage.UserId
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

        base.OnAppearing();
    }
}