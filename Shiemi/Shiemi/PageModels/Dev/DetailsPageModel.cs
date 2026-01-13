using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.Models;
using Shiemi.ViewModels;
using Shiemi.Dtos;
using Shiemi.Utilities.HubClients;
using Shiemi.Storage;
using Shiemi.Services;

namespace Shiemi.PageModels.Dev;

[QueryProperty(nameof(CurrentDev), "CurrentDev")]
public partial class DetailsPageModel(
        RoomClient roomServ,
        DevService devServ
        ) : BasePageModel
{
    private readonly RoomClient _roomServ = roomServ;
    private readonly DevService _devServ = devServ;

    [ObservableProperty]
    private DevModel? currentDev;

    [ObservableProperty]
    private bool isSendingChat;
    [ObservableProperty]
    private string sendChatText = string.Empty;
    [ObservableProperty]
    private bool isPageLoading;
    [ObservableProperty]
    private bool isPageExiting;
    [ObservableProperty]
    private bool isNotLoggedInUser;

    [ObservableProperty]
    private ObservableRangeCollection<MessageViewModel> messages = [];

    async partial void OnIsPageExitingChanged(bool value)
        => await PageIsExiting(value);
    async Task PageIsExiting(bool value)
    {
        if (value is false) return;
        try
        {
            Messages.Clear();
            if (_roomServ._hub is null) return;

            await _roomServ.DisconnectWebSocket();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsPageExiting = false;
        }
    }

    async partial void OnIsPageLoadingChanged(bool value)
        => await PageIsLoading(value);
    async Task PageIsLoading(bool value)
    {
        if (value is false) return;

        try
        {
			// check if logged in user owns this page
            DevDto loggedInUserDevDto = await _devServ.GetByUserId(UserStorage.UserId);
            if (loggedInUserDevDto.Id == CurrentDev.Id)
            {
                Debug.WriteLine("users own dev profile!");
                IsNotLoggedInUser = false;
                return;
            }

            int roomId = await _roomServ.GetPrivateRoom(
                    UserStorage.UserId,
					0,
                    CurrentDev.Id,
                    RoomTypes.DEV);
            if (roomId is 0)
            {
                await Shell.Current.DisplayAlertAsync(
                        "Live Room failure",
                        "Couldnot create live chat room",
                        "Ok");
                await Shell.Current.GoToAsync("..");
                return;
            }

            UserStorage.RoomId = roomId;
            await _roomServ.InitSignalR(Messages, roomId,RoomTypes.DEV);
            IsNotLoggedInUser = true;
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

    async partial void OnIsSendingChatChanged(bool value)
        => await SendingChat(value);
    async Task SendingChat(bool value)
    {
        if (value is false) return;
        try
        {
            await _roomServ.SendChat(new SendMessageDto(
                        SendChatText,
                        DateTime.UtcNow.ToLocalTime(),
                        UserStorage.UserId,
                        0,
                        UserStorage.RoomId
                        ));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsSendingChat = false;
        }
    }
}
