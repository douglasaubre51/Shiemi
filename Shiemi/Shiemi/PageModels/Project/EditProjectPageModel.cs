using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Dtos;
using Shiemi.Services;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Project;

[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class EditProjectPageModel(
    ProjectService projectServ) : BasePageModel
{
    private readonly ProjectService _projectServ = projectServ;

    [ObservableProperty]
    private ProjectsPageProjectViewModel currentProject;

    [RelayCommand]
    async Task EditCurrentProject()
    {
        await _projectServ.EditProject(new EditProjectDto(
            CurrentProject.Id,
            CurrentProject.Title,
            CurrentProject.ShortDesc,
            CurrentProject.Description));

        await Shell.Current.GoToAsync("..");
    }
}
