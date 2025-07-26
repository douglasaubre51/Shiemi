using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class MarketPlaceVM : BaseVM
    {
        //di
        readonly ProjectService _projectService;

        // collection
        [ObservableProperty]
        ObservableCollection<ProjectModel>? projects;

        public MarketPlaceVM(ProjectService projectService)
        {
            _projectService = projectService;
            Projects = new();
        }

        // user selected a project
        [RelayCommand]
        async Task GoToProjectInfoView(ProjectModel selectedProject)
        {
            if (IsBusy is true) return;

            try
            {
                await Shell.Current.GoToAsync(
                    nameof(ProjectInfoView),
                    true,
                    new Dictionary<string, object>
                    {
                    { "Project",selectedProject }
                    }
                    );
            }
            catch (Exception e)
            {
                Debug.WriteLine($"goto projectinfo view error: {e}");
            }
        }

        // sort:price low to high
        [RelayCommand]
        void SortPriceLowToHigh()
        {
            if (IsBusy is true) return;
            if (Projects is null) return;

            IsBusy = true;
            Projects = Projects.OrderBy(e => e.Price).ToObservableCollection();
            IsBusy = false;
        }
        // sort:price high to low
        [RelayCommand]
        void SortPriceHighToLow()
        {
            if (IsBusy is true) return;
            if (Projects is null) return;

            IsBusy = true;
            Projects = Projects.OrderByDescending(e => e.Price).ToObservableCollection();
            IsBusy = false;
        }
        // sort:createddate low to high
        [RelayCommand]
        void SortCreatedDateLowToHigh()
        {
            if (IsBusy is true) return;
            if (Projects is null) return;

            IsBusy = true;
            Projects = Projects.OrderBy(e => e.CreatedAt).ToObservableCollection();
            IsBusy = false;
        }
        // sort:createddate high to low
        [RelayCommand]
        void SortCreatedDateHighToLow()
        {
            if (IsBusy is true) return;
            if (Projects is null) return;

            IsBusy = true;
            Projects = Projects.OrderByDescending(e => e.CreatedAt).ToObservableCollection();
            IsBusy = false;
        }

        // load projects into collection
        public async Task LoadProjectsAsync()
        {
            IsBusy = false;

            try
            {
                Projects = await _projectService.GetAllProjects();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"loadprojectsasync error!: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // refresh
        [RelayCommand]
        async Task RefreshCollection()
        {
            if (IsBusy is true) return;

            IsBusy = true;
            try
            {
                var dto = await _projectService.GetAllProjects();
                if (dto is null) return;

                // success
                Projects = dto;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"refreshcollection error!: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
