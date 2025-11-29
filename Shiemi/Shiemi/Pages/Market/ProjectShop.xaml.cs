using Shiemi.PageModels.Market;
using Shiemi.Services;
using System.Diagnostics;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var pageModel = BindingContext as ProjectShopPageModel;

        try
        {
            pageModel!.ProjectCollection.Clear();
            var projects = await _projectService.GetAll();

            //pageModel!.ProjectCollection.Add(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ProjectShop error: {ex.Message}");
        }
    }
}