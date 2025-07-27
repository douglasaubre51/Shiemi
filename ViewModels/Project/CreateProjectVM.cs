using CommunityToolkit.Mvvm.Input;
using Shiemi.Dto.Project;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.ViewModels
{
    public partial class CreateProjectVM : BaseVM
    {
        // form binders
        [ObservableProperty]
        string title;
        [ObservableProperty]
        string shortDescription;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string price;

        // form validation message binders
        [ObservableProperty]
        string titleMessage;
        [ObservableProperty]
        string shortDescriptionMessage;
        [ObservableProperty]
        string descriptionMessage;
        [ObservableProperty]
        string priceMessage;

        // validity props
        [ObservableProperty]
        bool isPriceValid;

        // di
        readonly ProjectService _projectService;

        public CreateProjectVM(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [RelayCommand]
        async Task TriggerCreateNewProject()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            try
            {
                bool formValidationResult = FormValidator();
                if (!formValidationResult) return;

                // convert string price to decimal
                decimal cost = decimal.Parse(Price);

                // validated, pack data
                string userId = StorageService.GetUserId();
                ProjectDto project = new()
                {
                    UserId = userId,
                    Title = Title,
                    Price = cost,
                    ShortDescription = ShortDescription,
                    Description = Description
                };

                // call service
                bool result = await _projectService.AddProject(project);
                if (result is false)
                {
                    await Shell.Current
                        .DisplayAlertAsync(
                        "create project error",
                        "error creating new project!",
                        "ok"
                        );
                    return;
                }

                // success
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"createproject error: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // pick and upload photo
        [RelayCommand]
        void TriggerFilePicker()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            try
            {
                Debug.WriteLine("file picker");
            }
            catch (Exception e)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        bool FormValidator()
        {
            TitleMessage = string.Empty;
            ShortDescriptionMessage = string.Empty;
            DescriptionMessage = string.Empty;
            PriceMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Title))
            {
                TitleMessage += "required!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ShortDescription))
            {
                ShortDescriptionMessage += "required!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                DescriptionMessage += "required!";
                return false;
            }

            if (IsPriceValid is true)
            {
                PriceMessage += "price is in digits!";
                return false;
            }

            return true;
        }
    }
}
