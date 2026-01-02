using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.User;

public partial class HomePageModel(
    ProjectService projectServ,
    UserService userServ
) : BasePageModel
{
    private readonly ProjectService _projectServ = projectServ;
    private readonly UserService _userServ = userServ;

    [ObservableProperty] private bool isPageExiting;
    [ObservableProperty] private bool isPageLoading;
    [ObservableProperty] private ObservableRangeCollection<GalleryViewModel> joinedProjectCollection = [];
    [ObservableProperty] private ObservableRangeCollection<GalleryViewModel> myProjectCollection = [];

    // fetch user joined projects!
    private async Task InitJoinedProjectsCollection()
    {
        var projects = await projectServ.GetUserJoinedProjects(UserStorage.UserId);
        if (projects!.Count is 0) return;

        List<GalleryViewModel> galleryModels = [];
        foreach (var i in projects)
        {
            var model = new GalleryViewModel(
                i.Id,
                CoverBlurb: i.ShortDesc,
                CoverTitle: i.Title
            );
            galleryModels.Add(model);
        }

        JoinedProjectCollection.ReplaceRange(galleryModels);
    }

    // fetch user made projects!
    private async Task InitMyProjectsCollection()
    {
        var projects = await projectServ.GetAllByUser(UserStorage.UserId);
        if (projects!.Count is 0) return;

        List<GalleryViewModel> galleryModels = [];
        foreach (var i in projects)
        {
            var model = new GalleryViewModel(
                i.Id,
                CoverBlurb: i.ShortDesc,
                CoverTitle: i.Title
            );
            galleryModels.Add(model);
        }

        MyProjectCollection.ReplaceRange(galleryModels);
    }

    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;

        try
        {
            // just user id init! belongs at start page!
            if (DataStorage.Get("UserId") is not "")
            {
                var dto = await _userServ.GetUserId(DataStorage.Get("UserId"));
                UserStorage.UserId = dto!.Id;
            }

            await InitMyProjectsCollection();
            await InitJoinedProjectsCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsPageLoading = false;
        }
    }
}