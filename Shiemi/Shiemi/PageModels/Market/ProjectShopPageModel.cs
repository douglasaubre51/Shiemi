using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Pages.Market;
using Shiemi.ViewModels;
using System.Diagnostics;

namespace Shiemi.PageModels.Market;

public partial class ProjectShopPageModel : BasePageModel
{
    public ObservableRangeCollection<ProjectViewModel> ProjectCollection { get; set; } = [];

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
        catch (Exception ex)
        {
            Debug.WriteLine($"GoToProjectDetails error: {ex.Message}");
        }
    }
}
