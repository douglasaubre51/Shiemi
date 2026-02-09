using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.Models;
using Shiemi.Utilities.HubClients;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Dev;

[QueryProperty(nameof(CurrentClient), "CurrentClient")]
public partial class ChatPageModel(
    RoomClient roomClient
) : BasePageModel
{
    private readonly RoomClient _roomClient = roomClient;
    [ObservableProperty]
    private ProfileCardModel currentClient;

    [ObservableProperty]
    private ObservableRangeCollection<MessageViewModel> messages = [];

    [ObservableProperty]
    private bool isPageLoading;
    [ObservableProperty]
    private bool isPageExiting;

    async partial void OnIsPageLoadingChanged(bool value)
    {
        try
        {
            if (value is false) return;
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
