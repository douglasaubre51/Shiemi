using CommunityToolkit.Mvvm.Input;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class ProjectVM : BaseVM
    {
        // di
        readonly ProjectService _projectService;

        public ProjectVM(ProjectService projectService)
        {
            Title = "Projects";

            _projectService = projectService;
        }

        // on add project btn clicked!
        [RelayCommand]
        async Task TriggerNewProject()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            try
            {
                await _projectService.CreateNewProject();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"newproject error:{e}");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
