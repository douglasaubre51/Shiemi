using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Models;
using Shiemi.Models.ProjectModels;
using Shiemi.Pages.Market;
using Shiemi.Services;

namespace Shiemi.PageModels.Market;

[QueryProperty(nameof(ProjectVM), nameof(ProjectVM))]
public partial class ProjectDetailsPageModel(ProjectService projectServ) : BasePageModel
{
    private readonly ProjectService _projectServ = projectServ;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private string tag1;
    [ObservableProperty]
    private string tag2;
    [ObservableProperty]
    private string tag3;

    [ObservableProperty]
    private ProjectShopCardModel projectVM;

    [ObservableProperty]
    private ObservableRangeCollection<Review> reviewList = [];

    [ObservableProperty]
    private bool notOwner = true;
    [ObservableProperty]
    private bool notAllowedToReview = false;


    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            List<string> tags = await _projectServ.GetTags(ProjectVM!.ProjectId);
            if (tags is null || tags.Count == 0) return;

            Tag1 = tags[0];
            Tag2 = tags[1];
            Tag3 = tags[2];
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Details page loading error: " + ex.Message);
        }
        finally
        {
            IsPageLoading = false;
        }
    }


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
