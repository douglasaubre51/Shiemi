using Shiemi.PageModels.Market;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages;

public partial class PrivateRoom : ContentPage
{
    private readonly RoomService _roomService;

    public PrivateRoom(
        PrivateRoomPageModel pageModel,
        RoomService roomService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _roomService = roomService;
    }

    protected override async void OnAppearing()
    {
        var pageModel = BindingContext as PrivateRoomPageModel;
        try
        {
            int roomId = await _roomService.GetPrivateRoom(
                UserStorage.UserId,
                pageModel!.Project.Id
                );
            Debug.WriteLine("room id: " + roomId);
            await _roomService.InitSignalR(
                pageModel!.MessageCollection,
                roomId
                );
        }
        catch (Exception ex) { Debug.WriteLine($"PrivateRoom init error: {ex.Message}"); }

        base.OnAppearing();
    }

    protected override async void OnDisappearing()
    {
        try
        {
            Debug.WriteLine("closing websocket!");
            await _roomService.DisconnectWebSocket();
        }
        catch (Exception ex) { Debug.WriteLine($"SignalR Dispose error: {ex}"); }

        base.OnDisappearing();
    }
}