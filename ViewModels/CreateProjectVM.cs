using CommunityToolkit.Mvvm.Input;
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
        DateTime endsAt;

        // form validation message binders
        [ObservableProperty]
        string titleMessage;
        [ObservableProperty]
        string shortDescriptionMessage;
        [ObservableProperty]
        string descriptionMessage;

        [RelayCommand]
        async Task TriggerCreateNewProject()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            try
            {
                bool formValidationResult = FormValidator();
                if (!formValidationResult) return;

                Debug.WriteLine("create new project");
            }
            catch (Exception e)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task TriggerFilePicker()
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

            return true;
        }
    }
}
