using CommunityToolkit.Mvvm.ComponentModel;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Project;

[QueryProperty(nameof(CurrentProject), nameof(ProjectsPageProjectViewModel))]
public partial class DetailsPageModel : BasePageModel
{
    [ObservableProperty]
    private ProjectsPageProjectViewModel? currentProject;
}
