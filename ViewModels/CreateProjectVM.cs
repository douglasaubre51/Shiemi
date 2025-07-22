using CommunityToolkit.Mvvm.Input;

namespace Shiemi.ViewModels
{
    public partial class CreateProjectVM : BaseVM
    {
        [RelayCommand]
        async Task TriggerCreateNewProject() { }
    }
}
