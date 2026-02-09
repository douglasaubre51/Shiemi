using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Project;

[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class DetailsPageModel : BasePageModel
{
    [ObservableProperty]
    private ProjectsPageProjectViewModel? currentProject;

    [RelayCommand]
    async Task GoToChats()
        => await Shell.Current.GoToAsync(
            "ChatRooms",
            true,
            new Dictionary<string, object>
            {
                {"SelectedProject",CurrentProject! }
            });

    [RelayCommand]
    async Task GoToAddDevelopers()
        => await Shell.Current.GoToAsync("AddDevs",
            true,
            new Dictionary<string, object>()
            {
                {"CurrentProject",CurrentProject!}
            });
}
