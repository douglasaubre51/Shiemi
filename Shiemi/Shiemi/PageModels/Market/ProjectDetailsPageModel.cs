using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Models;
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
    private ObservableRangeCollection<Review> reviewList = [];

    [ObservableProperty]
    private bool notOwner = true;
    [ObservableProperty]
    private bool notAllowedToReview = false;

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
                {
                    { "ProjectVM", ProjectVM }
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"GoToPrivateRoom error: ${ex.Message}");
        }
    }
}
