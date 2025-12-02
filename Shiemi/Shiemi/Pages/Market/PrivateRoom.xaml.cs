using Shiemi.Dtos;
using Shiemi.PageModels.Market;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using System.Diagnostics;

namespace Shiemi.Pages.Market;

public partial class PrivateRoom : ContentPage
{
    private readonly RoomClient _roomService;
    private readonly UserService _userService;

    public PrivateRoom(
        PrivateRoomPageModel pageModel,
        RoomClient roomService,
        UserService userService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _roomService = roomService;
        _userService = userService;
    }


    // init SignalR on page load!

    protected override async void OnAppearing()
    {
        var pageModel = BindingContext as PrivateRoomPageModel;
        if (pageModel is null)
        {
            Debug.WriteLine("PrivateRoomPageModel BindingContext is null!");
            return;
        }

        try
        {
            int roomId = await _roomService.GetPrivateRoom(
               UserStorage.UserId,
               pageModel.CurrentProjectVM.Id
               );
            UserStorage.RoomId = roomId;

            await _roomService.InitSignalR(
               pageModel!.MessageCollection,
               roomId
               );

            UserDto? user = await _userService.GetUserById(pageModel.CurrentProjectVM.UserId);
            pageModel.Sender = user!.FirstName + " " + user.LastName;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"PrivateRoom init error: {ex.Message}");
        }

        base.OnAppearing();
    }


    // destroy signalR on page exit!

    protected override async void OnDisappearing()
    {
        try
        {
            Debug.WriteLine("closing websocket!");
            await _roomService.DisconnectWebSocket();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SignalR Dispose error: {ex}");
        }

        base.OnDisappearing();
    }
}