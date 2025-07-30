using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using Shiemi.Dto.Project;
using Shiemi.Models;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.ViewModels.Project
{
    [QueryProperty("Project", "Project")]
    public partial class EditProjectVM : BaseVM
    {
        // di
        readonly ProjectService _projectService;

        // passed object
        [ObservableProperty]
        ProjectModel project;

        // binders
        [ObservableProperty]
        string title;
        [ObservableProperty]
        string shortDescription;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string price;

        public EditProjectVM(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [RelayCommand]
        async Task SaveEditedProfile()
        {
            if (IsBusy is true) return;

            try
            {
                ProjectDto dto = new()
                {
                    Title = Title,
                    ShortDescription = ShortDescription,
                    Description = Description,
                    Price = Decimal128.Parse(Price)
                };

                await _projectService.EditProfile(dto);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"SaveEditedProfile error: {e}");
            }
        }
    }
}
