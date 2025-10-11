using CommunityToolkit.Mvvm.Input;
using Shiemi.Pages;

namespace Shiemi.PageModels;

public partial class ProjectsPageModel : BasePageModel
{
    public ProjectsPageModel()
    {
        Title = "Projects";
    }

    [RelayCommand]
    async Task GoToCreateProject()
        => await Shell.Current.GoToAsync(nameof(CreateProject));
}
