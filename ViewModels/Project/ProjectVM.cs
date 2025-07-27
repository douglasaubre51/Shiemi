using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Views.Project;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class ProjectVM : BaseVM
    {
        readonly string userId;
        // di
        readonly ProjectService _projectService;
        // collections
        [ObservableProperty]
        ObservableCollection<ProjectModel>? projects = new();

        public ProjectVM(ProjectService projectService)
        {
            // fetch userid
            userId = StorageService.GetUserId();
            _projectService = projectService;
        }

        // on project selected!
        [RelayCommand]
        async Task GoToProjectDetailsView(ProjectModel selectedProject)
        {
            if (IsBusy is true) return;
            try
            {
                Dictionary<string, object>? project = new()
                    {
                        {"Project",selectedProject }
                    };
                await Shell.Current.GoToAsync(
                    nameof(ProjectDetailsView),
                    true,
                    project
                    );
            }
            catch (Exception e)
            {
                Debug.WriteLine($"goto projectdetailsview error: {e}");
            }
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
                // success
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
