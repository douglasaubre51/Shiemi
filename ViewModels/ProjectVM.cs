using CommunityToolkit.Mvvm.Input;

namespace Shiemi.ViewModels
{
    public partial class ProjectVM : BaseVM
    {
        public ProjectVM()
        {
            Title = "Projects";
        }

        // on add project btn clicked!
        [RelayCommand]
        async Task GoToCreateProjectView()
        {
            if (IsBusy is true) return;

            // go to createprojectview
            await Shell.Current.GoToAsync("CreateProjectView");
        }
    }
}
