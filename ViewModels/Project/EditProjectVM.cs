using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using Shiemi.Dto.Project;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Views;
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

            IsBusy = true;

            try
            {
                ProjectDto dto = new()
                {
                    UserId = Project.UserId,
                    Title = Title,
                    ShortDescription = ShortDescription,
                    Description = Description,
                    Price = Decimal128.Parse(Price)
                };

                Debug.WriteLine(dto.UserId);
                Debug.WriteLine(dto.Title);
                Debug.WriteLine(dto.ShortDescription);
                Debug.WriteLine(dto.Description);
                Debug.WriteLine(dto.Price);

                bool isSuccess = await _projectService.EditProfile(dto);
                if (!isSuccess)
                {
                    await Shell.Current.DisplayAlertAsync(
                        "error",
                        "cannot update project details!",
                        "ok"
                        );
                    return;
                }

                // success
                await Shell.Current.DisplayAlertAsync(
                        "success",
                        "project updated successfully!",
                        "ok"
                    );

                await Shell.Current.GoToAsync(
                    nameof(ProjectView),
                    true
                    );
            }
            catch (Exception e)
            {
                Debug.WriteLine($"SaveEditedProfile error: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
