using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;
using Shiemi.Pages;
using System.Diagnostics;

namespace Shiemi.PageModels;

[QueryProperty(nameof(Project), "Project")]
public partial class ProjectDetailsPageModel : BasePageModel
{
    [ObservableProperty]
    private Project project;
    [ObservableProperty]
    private bool notOwner = true;

    public ProjectDetailsPageModel() => Title = "Project Details";

    [RelayCommand]
    async Task GoToPrivateRoom()
    {
        try
        {
            await Shell.Current.GoToAsync(
                nameof(PrivateRoom),
                true,
                new Dictionary<string, object>
                { { "Project", Project } });
        }
        catch (Exception ex) { Debug.WriteLine($"GoToPrivateRoom error: ${ex.Message}"); }
    }
}
