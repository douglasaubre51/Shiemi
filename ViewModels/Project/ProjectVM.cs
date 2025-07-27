using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class ProjectVM : BaseVM
    {
        // services
        readonly ProjectService _projectService;

        // collections

        [ObservableProperty]
        ObservableCollection<ProjectModel>? projects = new();

        readonly string userId;

        public ProjectVM(ProjectService projectService)
        {
            // fetch userid
            userId = StorageService.GetUserId();

            //di
            _projectService = projectService;

        }


        // on add project btn clicked!
        [RelayCommand]
        async Task GoToCreateProjectView()
        {
            if (IsBusy is true) return;

            // go to createprojectview
            await Shell.Current.GoToAsync("CreateProjectView");
        }


        // store all projects
        [RelayCommand]
        public async Task FillProjectCollection()
        {
            IsBusy = true;

            try
            {
                var collection = await _projectService.GetAllProjectsById(userId);
                if (collection is null) return;

                Projects = collection;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Add to projectcollection error: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
