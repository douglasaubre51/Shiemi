using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Pages.Market;
using Shiemi.Services;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Project;

[QueryProperty(nameof(IsJoinedProject), "IsJoinedProject")]
[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class DetailsPageModel(
    ChannelService channelService) : BasePageModel
{
    private readonly ChannelService _channelService = channelService;

    [ObservableProperty]
    private bool isJoinedProject;

    [ObservableProperty]
    private ProjectsPageProjectViewModel? currentProject;

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
        => await Shell.Current.GoToAsync(
            "ChatRooms",
            true,
            new Dictionary<string, object>
            {
                {"SelectedProject",CurrentProject! }
            });

    [RelayCommand]
    async Task GoToAddDevelopers()
        => await Shell.Current.GoToAsync("AddDevs",
            true,
            new Dictionary<string, object>()
            {
                {"CurrentProject",CurrentProject!}
            });
}
