using CommunityToolkit.Mvvm.Input;
using Shiemi.Pages;
using Shiemi.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.PageModels;

public partial class ProjectShopPageModel : BasePageModel
{
    public ObservableCollection<ProjectViewModel> ProjectCollection { get; set; } = [];
    public ProjectShopPageModel()
        => Title = "Project Shop";

    [RelayCommand]
    async Task GoToProjectDetails(ProjectViewModel projectVM)
    {
        try
        {
            await Shell.Current.GoToAsync(
               $"{nameof(ProjectDetails)}",
               true,
               new Dictionary<string, object>
               { { "ProjectVM", projectVM! }
               });
        }
        catch (Exception ex) { Debug.WriteLine($"GoToProjectDetails error: {ex.Message}"); }
    }
}
