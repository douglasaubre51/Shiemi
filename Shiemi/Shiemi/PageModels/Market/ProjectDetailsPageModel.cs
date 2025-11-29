using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Pages.Market;
using Shiemi.ViewModels;
using System.Diagnostics;

namespace Shiemi.PageModels.Market;

[QueryProperty(nameof(ProjectVM), nameof(ProjectVM))]
public partial class ProjectDetailsPageModel : BasePageModel
{
    [ObservableProperty]
    private ProjectViewModel projectVM;
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
                { { "Project", ProjectVM } });
        }
        catch (Exception ex) { Debug.WriteLine($"GoToPrivateRoom error: ${ex.Message}"); }
    }
}
