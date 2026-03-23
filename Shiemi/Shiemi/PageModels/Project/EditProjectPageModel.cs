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
    private bool isPageLoading;

    [ObservableProperty]
    private string tag1 = string.Empty;
    [ObservableProperty]
    private string tag2 = string.Empty;
    [ObservableProperty]
    private string tag3 = string.Empty;

    [ObservableProperty]
    private ProjectsPageProjectViewModel currentProject;

    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            List<string> tags = [];
            tags = await _projectServ.GetTags(currentProject.Id);
            if (tags.Count is 0) return;

            Tag1 = tags[0];
            Tag2 = tags[1];
            Tag3 = tags[2];
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Project error",
                "Couldnot load project tags!",
                "Ok");
        }
        finally
        {
            IsPageLoading = false;
        }
    }

    [RelayCommand]
    async Task EditCurrentProject()
    {
        await _projectServ.EditProject(new EditProjectDto(
            CurrentProject.Id,
            CurrentProject.Title,
            CurrentProject.ShortDesc,
            CurrentProject.Description,
            new List<string>
            {
                Tag1,
                Tag2,
                Tag3
            }));

        await Shell.Current.GoToAsync("..");
    }
}
