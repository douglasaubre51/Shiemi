using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Pages;
using Shiemi.ViewModels;

namespace Shiemi.PageModels;

public partial class ProjectsPageModel : BasePageModel
{
    [ObservableProperty]
    public ObservableRangeCollection<ProjectsPageProjectViewModel> projectCollection = [];

    public ProjectsPageModel()
        => Title = "My Projects";

    [RelayCommand]
    async Task GoToCreateProject()
        => await Shell.Current.GoToAsync(nameof(CreateProject));
}
