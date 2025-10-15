using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using System.Diagnostics;

namespace Shiemi.PageModels;

[QueryProperty("Project", "Project")]
public partial class ProjectDetailsPageModel : BasePageModel
{
    [ObservableProperty]
    private Project project;

    public ProjectDetailsPageModel()
    {
        Title = "Project Details";
    }

    [RelayCommand]
    async Task GoToPrivateRoom()
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(PrivateRoom));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"GoToPrivateRoom error: ${ex.Message}");
        }
    }
}
