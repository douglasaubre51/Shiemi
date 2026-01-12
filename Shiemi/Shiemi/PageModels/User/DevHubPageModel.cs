using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
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
    private ObservableRangeCollection<ProfileCardModel> clientProfiles = [];

    private readonly RoomClient _roomClient = roomClient;

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
        IsPageExiting = false;
    }
    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;

        try
        {
            List<Dtos.GetDevRoomDto>? profiles = await _roomClient.GetAllDevRoomDtos(UserStorage.UserId);
            if (profiles!.Count is 0) return;

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

                Debug.WriteLine(profile.ProfilePhotoURI);
            }
            ClientProfiles.AddRange(profileCards);
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
