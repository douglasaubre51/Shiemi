using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Models;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;

namespace Shiemi.PageModels.Chat;

[QueryProperty(nameof(CurrentProjectTitle), "SelectedProjectTitle")]
[QueryProperty(nameof(CurrentChannelId), "SelectedChannelId")]
public partial class ChannelsPageModel(
    ChannelClient channelClient) : BasePageModel
{
    [ObservableProperty]
    private string currentProjectTitle = string.Empty;

    [ObservableProperty]
    private int currentChannelId;

    private readonly ChannelClient _channelClient = channelClient;

    [ObservableProperty]
    private bool isPageLoading;
    [ObservableProperty]
    private bool isPageExiting;

    [ObservableProperty]
    private ObservableRangeCollection<Message> messageCollection = [];

    [ObservableProperty]
    private string channelTitle = "Channel Title";

    [ObservableProperty]
    private string chatBoxText = string.Empty;

    async partial void OnIsPageExitingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            await _channelClient.StopClient();
            CurrentChannelId = 0;
            MessageCollection.Clear();
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
        }
        finally
        {
            IsPageExiting = false;
        }
    }

    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            await _channelClient.StartClient(
                CurrentChannelId,
                MessageCollection
                );
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
            await Shell.Current.DisplayAlertAsync(
                "Channel loading error",
                "Couldnot load selected channel!",
                "Go back");

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsPageLoading = false;
        }
    }

    [RelayCommand]
    async Task SendChat()
    {
        if (string.IsNullOrWhiteSpace(ChatBoxText)) return;

        try
        {
            await _channelClient.SendChat(new Dtos.SendMessageDto(
                ChatBoxText,
                DateTime.UtcNow,
                UserId: UserStorage.UserId,
                ChannelId: CurrentChannelId,
                RoomId: 0
                ));

            ChatBoxText = string.Empty;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlertAsync(
                "Send message error",
                "Couldnot send message !",
                "Ok");

            return;
        }
    }
}
