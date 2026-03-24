using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Pages.Market;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Project;

// Project Details in Home !

[QueryProperty(nameof(IsJoinedProject), "IsJoinedProject")]
[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class DetailsPageModel(
    ChannelService channelService,
    ProjectService projectServ) : BasePageModel
{
    private readonly ProjectService _projectServ = projectServ;
    private readonly ChannelService _channelService = channelService;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private bool isJoinedProject;

    [ObservableProperty]
    private string tag1 = string.Empty;
    [ObservableProperty]
    private string tag2 = string.Empty;
    [ObservableProperty]
    private string tag3 = string.Empty;

    [ObservableProperty]
    private ProjectsPageProjectViewModel? currentProject;


    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            List<string> tags = await _projectServ.GetTags(CurrentProject!.Id);
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
    async Task GoToEditProject()
    {
        await Shell.Current.GoToAsync(
            "EditProject",
            true,
            new Dictionary<string, object>()
            {
                {"CurrentProject", CurrentProject! }
            });
    }

    [RelayCommand]
    async Task DeleteProject()
    {
        var response = await Shell.Current.DisplayAlertAsync(
            "Warning",
            "Do you want to delete this project? It can't be undone!",
            "Yes",
            "No");
        if (response is false) return;

        await _projectServ.DeleteProject(CurrentProject!.Id);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task LeaveProject()
    {
        var response = await Shell.Current.DisplayAlertAsync(
            "Warning",
            "Do you want to leave this project?",
            "Yes",
            "No");
        if (response is false) return;

        await _projectServ.RemoveDevFromProject(CurrentProject!.Id, UserStorage.UserId);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task GoToChannel()
    {
        await Shell.Current.GoToAsync(
           "Channels",
           true,
           new Dictionary<string, object>()
           {
                { "SelectedChannelId", CurrentProject!.ChannelId },
                { "SelectedProjectTitle",CurrentProject.Title }
           });
    }

    [RelayCommand]
    async Task GoToPrivateRoom()
    {
        var viewModel = new ProjectViewModel
        (
            Id: CurrentProject!.Id,
            Title: CurrentProject.Title,
            ShortDesc: CurrentProject.ShortDesc,
            Description: CurrentProject.Description,
            CreatedAt: CurrentProject.CreatedAt,
            Cost: 0,
            UserId: CurrentProject.UserId,
            ChannelId: CurrentProject.ChannelId,
            [],
            [],
            []
        );

        await Shell.Current.GoToAsync(
            nameof(PrivateRoom),
            true,
            new Dictionary<string, object>
            {
                    { "ProjectVM", viewModel! }
            });
    }

    [RelayCommand]
    async Task GoToChats()
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
            await Shell.Current.GoToAsync(
                "ManagePrivateChats",
                true,
                new Dictionary<string, object>
                {
                {"SelectedProject",CurrentProject! }
                });

        else
            await Shell.Current.GoToAsync(
                "ChatRooms",
                true,
                new Dictionary<string, object>
                {
                {"SelectedProject",CurrentProject! }
                });
    }

    [RelayCommand]
    async Task GoToAddDevelopers()
        => await Shell.Current.GoToAsync("AddDevs",
            true,
            new Dictionary<string, object>()
            {
                {"CurrentProject",CurrentProject!}
            });
}
