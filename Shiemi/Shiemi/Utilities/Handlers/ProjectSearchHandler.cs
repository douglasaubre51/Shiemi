using Shiemi.Dtos;
using Shiemi.Services;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Utilities.Handlers;

public class ProjectSearchHandler : SearchHandler
{
    private readonly ProjectService _projectService;

    public object? PageContext { get; set; }
    public List<SearchProjectsDto> SearchProjectsCollection { get; set; } = [];

    public ProjectSearchHandler()
    {
        _projectService = Provider.GetService<ProjectService>()!;
    }

    protected async override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        try
        {
            if (newValue.Length is 0) return;

            if (newValue[0] is '#')
            {
                Debug.WriteLine("search word: " + newValue);

                string tag = newValue.Substring(1);
                Debug.WriteLine("search tag: " + tag);

                List<SearchProjectsDto>? tagResults = await _projectService.GetSearchedProjectsByTags(tag);
                if (tagResults is null || tagResults.Count == 0)
                {

                    ItemsSource = new List<SearchProjectsDto>();
                    return;
                }

                ItemsSource = tagResults;
            }
            else
            {
                Debug.WriteLine($"text: {newValue}");
                List<SearchProjectsDto>? results = await _projectService.GetSearchedProjects(newValue);
                if (results is null || results.Count == 0)
                    return;

                ItemsSource = results;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    protected async override void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        try
        {
            SearchProjectsDto selectedProject = (SearchProjectsDto)item;

            ProjectDto? dbProject = await _projectService.GetById(selectedProject.Id);
            if (dbProject is null) return;

            var mapper = MapperProvider.GetMapper<ProjectDto, ProjectViewModel>();
            ProjectViewModel viewModel = mapper!.Map<ProjectViewModel>(dbProject);
            await Shell.Current.GoToAsync(
                "ProjectDetails",
                true,
                new Dictionary<string, object>
                {
                    { "ProjectVM", viewModel }
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
