using System.Diagnostics;
using Shiemi.Dtos;
using Shiemi.PageModels.Project;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Pages.Project;

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

            var mapper = MapperProvider.GetMapper<ProjectDto, ProjectsPageProjectViewModel>();
            if (mapper is null)
                return;

            // fetch user projects
            var projectList = await _projectService.GetAllByUser(
                UserStorage.UserId
                );
            var projects = mapper.Map<List<ProjectsPageProjectViewModel>>(projectList);

            // initialize project collection
            pageModel.ProjectCollection.Clear();
            pageModel.ProjectCollection.AddRange(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Loading UserProjectList error: {ex.Message}");
        }
    }

    private async void Project_CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("collectionview selectionchanged!");
        ProjectsPageProjectViewModel? project = e.CurrentSelection.SingleOrDefault() as ProjectsPageProjectViewModel;
        if (project is null)
        {
            Debug.WriteLine("selected project is null!");
            return;
        }
        Debug.WriteLine("selected project is null!");
    }
}