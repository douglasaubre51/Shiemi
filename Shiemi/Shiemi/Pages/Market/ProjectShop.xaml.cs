using Shiemi.Dtos;
using Shiemi.PageModels.Market;
using Shiemi.Services;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Pages.Market;

public partial class ProjectShop : ContentPage
{
    private readonly ProjectService _projectService;

    public ProjectShop(
        ProjectShopPageModel pageModel,
        ProjectService projectService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _projectService = projectService;
    }

    protected override void OnDisappearing()
    {
        Provider.GetTitleBarWidget()!.SearchBarIsEnabled = false;
        base.OnDisappearing();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var pageModel = BindingContext as ProjectShopPageModel;

        try
        {
            pageModel!.ProjectCollection.Clear();
            var projects = await _projectService.GetAll();

            var mapper = MapperProvider.GetMapper<ProjectDto, ProjectViewModel>();
            List<ProjectViewModel> projectViewModels = mapper!.Map<List<ProjectViewModel>>(projects);

            pageModel!.ProjectCollection.AddRange(projectViewModels);

            Provider.GetTitleBarWidget()!.SearchBarIsEnabled = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ProjectShop error: {ex.Message}");
        }
    }
}