using Shiemi.Models;

namespace Shiemi.ViewModels.Project
{
    [QueryProperty("Project", "Project")]
    public partial class ProjectDetailsVM : BaseVM
    {
        [ObservableProperty]
        ProjectModel project;
    }
}
