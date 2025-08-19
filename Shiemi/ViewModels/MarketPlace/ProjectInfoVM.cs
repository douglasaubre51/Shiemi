using Shiemi.Models;

namespace Shiemi.ViewModels
{
    [QueryProperty("Project", "Project")]
    public partial class ProjectInfoVM : BaseVM
    {
        // payload
        [ObservableProperty]
        ProjectModel project;

        public ProjectInfoVM()
        {
        }
    }
}
