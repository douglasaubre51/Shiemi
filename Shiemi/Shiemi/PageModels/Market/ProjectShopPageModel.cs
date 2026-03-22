using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Models.ProjectModels;
using Shiemi.Pages.Market;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Market;

public partial class ProjectShopPageModel : BasePageModel
{
    public ObservableRangeCollection<ProjectShopCardModel> ProjectCollection { get; set; } = [];

    public ProjectShopPageModel()
        => Title = "Project Shop";

    [RelayCommand]
    async Task GoToUserProfilePage(ProjectShopCardModel projectShopCard)
        => await Shell.Current.GoToAsync(
            "///Profile",
            true,
            new Dictionary<string, object>
            {
                { "IsWatchingProfile",true },
                { "UserId",projectShopCard.UserId}
            });

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
