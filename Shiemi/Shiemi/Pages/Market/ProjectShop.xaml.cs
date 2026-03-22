using Shiemi.Models.ProjectModels;
using Shiemi.PageModels.Market;
using Shiemi.Services;

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
        base.OnDisappearing();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var pageModel = BindingContext as ProjectShopPageModel;

        try
        {
            pageModel!.IsBusy = true;

            pageModel!.ProjectCollection.Clear();
            var projects = await _projectService.GetAll();
            if (projects is null || projects.Count == 0) return;

            List<ProjectShopCardModel> projectCards = [];
            foreach (var project in projects)
            {
                projectCards.Add(new ProjectShopCardModel
                {
                    ProjectId = project.Id,
                    UserId = project.UserId,
                    UserProfilePhoto = project.UserProfilePhoto,
                    Title = project.Title,
                    Short = project.ShortDesc,
                    Desc = project.Description,
                    Username = project.Username
                });
            }

            pageModel!.ProjectCollection.AddRange(projectCards);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ProjectShop error: {ex.Message}");
        }
        finally
        {
            pageModel!.IsBusy = false;
        }
    }
}