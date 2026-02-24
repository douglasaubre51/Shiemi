using CommunityToolkit.Mvvm.ComponentModel;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Project;

[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class EditProjectPageModel : BasePageModel
{
    [ObservableProperty]
    private ProjectsPageProjectViewModel currentProject;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private string projectTitleTextBox = string.Empty;

    [ObservableProperty]
    private string projectDescTextBox = string.Empty;

    [ObservableProperty]
    private string projectShortDescTextBox = string.Empty;

    partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        ProjectTitleTextBox = CurrentProject.Title;
        ProjectShortDescTextBox = CurrentProject.ShortDesc;
        ProjectDescTextBox = CurrentProject.Description;
    }
}
