using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;

namespace Shiemi.PageModels.User;

public partial class DevHubPageModel(
    RoomClient roomClient
) : BasePageModel
{
    [ObservableProperty]
    private bool isPageLoading;
    [ObservableProperty]
    private bool isPageExiting;

    [ObservableProperty]
    private ObservableRangeCollection<ProfileCardModel> devProfiles = [];
    [ObservableProperty]
    private ObservableRangeCollection<ProfileCardModel> clientProfiles = [];

    private readonly RoomClient _roomClient = roomClient;

    [RelayCommand]
    async Task DidSelectedDevProfileChange(ProfileCardModel selectedProfileCard)
    {
        try
        {
            await Shell.Current.GoToAsync(
                "ChatDev",
                true,
                new Dictionary<string, object>()
                {
                    { "CurrentClient",selectedProfileCard! }
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"DidSelectedDevProfileChange error: {ex.Message}");
        }
    }

    [RelayCommand]
    async Task DidSelectedClientProfileChange(ProfileCardModel selectedProfileCard)
    {
        try
        {
            await Shell.Current.GoToAsync(
                "ChatDev",
                true,
                new Dictionary<string, object>()
                {
                    { "CurrentClient",selectedProfileCard! }
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"DidSelectedClientProfileChange error: {ex.Message}");
        }
    }

    async partial void OnIsPageExitingChanged(bool value)
    {
        if (value is false) return;

        ClientProfiles.Clear();
        DevProfiles.Clear();
        IsPageExiting = false;
    }

    async Task FetchClientProfiles()
    {
        List<Dtos.GetDevRoomDto>? profiles = await _roomClient.GetAllDevRoomDtos(UserStorage.UserId);
        if (profiles is null || profiles.Count is 0) return;

        List<ProfileCardModel> profileCards = [];

        foreach (var p in profiles)
        {
            ProfileCardModel? profile = new ProfileCardModel
            (
                Id: p.ClientId,
                Username: p.ClientName,
                ProfilePhotoURI: p.ProfilePhotoURL,
                RoomId: p.RoomId
            );

            profileCards.Add(profile);
        }

        ClientProfiles.AddRange(profileCards);
    }
    async Task FetchDevProfiles()
    {
        // Fetch all dev profiles for Client !
        List<GetDevProfileForClientDto>? devProfiles = await _roomClient.GetAllDevProfilesForClient(UserStorage.UserId);
        if (devProfiles is null || devProfiles.Count is 0) return;

        List<ProfileCardModel> devProfileCards = [];

        foreach (var p in devProfiles)
        {
            ProfileCardModel? profile = new ProfileCardModel
            (
                Id: p.OwnerId,
                Username: p.OwnerName,
                ProfilePhotoURI: p.OwnerProfile,
                RoomId: p.RoomId
            );

            devProfileCards.Add(profile);
        }

        DevProfiles.AddRange(devProfileCards);
    }

    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;

        try
        {
            IsBusy = true;

            await FetchClientProfiles();
            await FetchDevProfiles();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsBusy = false;
            IsPageLoading = false;
        }
    }
}
