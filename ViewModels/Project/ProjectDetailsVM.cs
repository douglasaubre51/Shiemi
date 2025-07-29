using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Views.Project;

namespace Shiemi.ViewModels.Project
{
    [QueryProperty("Project", "Project")]
    public partial class ProjectDetailsVM : BaseVM
    {
        [ObservableProperty]
        ProjectModel project;

        [RelayCommand]
        async Task GoToEditProjectView()
        {
            if (IsBusy is true) return;

            await Shell.Current.GoToAsync(nameof(EditProjectView), true);
        }
    }
}
