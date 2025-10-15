using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Pages;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.PageModels;

public partial class ProjectShopPageModel : BasePageModel
{
    public ObservableCollection<Project> ProjectCollection { get; set; } = new();

    public ProjectShopPageModel()
    {
        Title = "Project Shop";
    }

    [RelayCommand]
    async Task GoToProjectDetails(Project project)
    {
        try
        {
            Debug.WriteLine($"{project is null}");
            await Shell.Current.GoToAsync(
               $"{nameof(ProjectDetails)}",
               true,
               new Dictionary<string, object>
               {
                { "Project", project! }
               }
               );
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"GoToProjectDetails error: {ex.Message}");
        }
    }
}
