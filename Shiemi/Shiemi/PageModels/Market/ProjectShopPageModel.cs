using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Pages;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.PageModels;

public partial class ProjectShopPageModel : BasePageModel
{
    public ObservableCollection<Project> ProjectCollection { get; set; } = [];
    public ProjectShopPageModel()
        => Title = "Project Shop";

    [RelayCommand]
    async Task GoToProjectDetails(Project project)
    {
        try
        {
            await Shell.Current.GoToAsync(
               $"{nameof(ProjectDetails)}",
               true,
               new Dictionary<string, object>
               { { "Project", project! }
               });
        }
        catch (Exception ex) { Debug.WriteLine($"GoToProjectDetails error: {ex.Message}"); }
    }
}
