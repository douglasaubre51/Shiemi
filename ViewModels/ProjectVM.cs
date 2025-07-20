using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

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
        void TriggerNewProject()
        {
            if (IsBusy is true) return;

            IsBusy = true;

            try
            {
            }
            catch (Exception e)
            {
                Debug.WriteLine($"new project error:{e}");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
