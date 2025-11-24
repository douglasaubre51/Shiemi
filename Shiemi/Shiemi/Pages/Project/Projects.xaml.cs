using Shiemi.Dtos;
using Shiemi.PageModels;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;
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
        try
        {
            var pageModel = BindingContext as ProjectsPageModel;
            if (pageModel is null)
                return;

            var projectList = await _projectService.GetAllByUser(
                UserStorage.UserId
                );
            var mapper = MapperProvider.GetMapper<ProjectDto, ProjectsPageProjectViewModel>();
            if (mapper is null)
                return;

            var projects = mapper.Map<List<ProjectsPageProjectViewModel>>(projectList);
            foreach (var p in projects)
            {
                Debug.WriteLine(p.Title);
                Debug.WriteLine(p.ShortDesc);
            }

            pageModel.ProjectCollection.Clear();
            pageModel.ProjectCollection.AddRange(projects);

            foreach (var p in pageModel.ProjectCollection)
            {
                Debug.WriteLine(p.Title);
                Debug.WriteLine(p.ShortDesc);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Loading UserProjectList error: {ex.Message}");
        }
    }
}