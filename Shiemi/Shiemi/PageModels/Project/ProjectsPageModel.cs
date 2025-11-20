using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Pages;
using Shiemi.ViewModels;

namespace Shiemi.PageModels;

public partial class ProjectsPageModel : BasePageModel
{
    public ObservableRangeCollection<ProjectViewModel> ProjectCollection { get; set; } = [];

    public ProjectsPageModel()
        => Title = "My Projects";

    [RelayCommand]
    async Task GoToCreateProject()
        => await Shell.Current.GoToAsync(nameof(CreateProject));
}
