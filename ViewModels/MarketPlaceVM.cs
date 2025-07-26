using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Services;
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

        // sort:price low to high
        [RelayCommand]
        async Task SortPriceLowToHigh()
        {
            if (IsBusy is true) return;
            if (Projects is null) return;

            IsBusy = true;
            Projects = Projects.OrderBy(e => e.Price).ToObservableCollection();
            IsBusy = false;
        }
        // sort:price high to low
        [RelayCommand]
        async Task SortPriceHighToLow()
        {
            if (IsBusy is true) return;
            if (Projects is null) return;

            IsBusy = true;
            Projects = Projects.OrderByDescending(e => e.Price).ToObservableCollection();
            IsBusy = false;
        }
        // sort:createddate low to high
        [RelayCommand]
        async Task SortCreatedDateLowToHigh()
        {
            if (IsBusy is true) return;
            if (Projects is null) return;

            IsBusy = true;
            Projects = Projects.OrderBy(e => e.CreatedAt).ToObservableCollection();
            IsBusy = false;
        }
        // sort:createddate high to low
        [RelayCommand]
        async Task SortCreatedDateHighToLow()
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
