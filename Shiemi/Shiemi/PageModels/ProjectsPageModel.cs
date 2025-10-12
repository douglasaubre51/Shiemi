using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Pages;
using System.Collections.ObjectModel;

namespace Shiemi.PageModels;

public partial class ProjectsPageModel : BasePageModel
{
    public ObservableCollection<Project> ProjectCollection { get; set; } = new();

    public ProjectsPageModel()
    {
        Title = "Projects";
    }

    [RelayCommand]
    async Task GoToCreateProject()
        => await Shell.Current.GoToAsync(nameof(CreateProject));
}
