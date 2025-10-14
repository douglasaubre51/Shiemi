using Shiemi.PageModels;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.Pages;

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
            var projects = await _projectService.GetAll();

            foreach (var p in projects!)
            {
                pageModel!.ProjectCollection.Add(p);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ProjectShop error: {ex.Message}");
        }
    }
}