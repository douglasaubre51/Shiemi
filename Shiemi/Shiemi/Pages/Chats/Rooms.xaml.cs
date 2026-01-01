using System.Diagnostics;
using Shiemi.Dtos;
using Shiemi.PageModels.Chat;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using Shiemi.ViewModels;

namespace Shiemi.Pages.Chats;

public partial class Rooms : ContentPage
{
    private readonly ChatService _chatService;
    private readonly UserService _userService;
    private readonly RoomClient _roomService;

    public Rooms(
            ChatService chatService,
            RoomsPageModel pageModel,
            UserService userService,
            RoomClient roomService
            )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _chatService = chatService;
        _userService = userService;
        _roomService = roomService;
    }

    // init signalR on page load !
    protected override async void OnAppearing()
    {
        try
        {
            var context = BindingContext as RoomsPageModel;
            if (context is null)
                return;

            var rooms = await _chatService.GetAllRooms();
            if (rooms is null)
            {
                Debug.WriteLine("Fetching Rooms: null");
                return;
            }

            context.ChatCollection.Clear();
            foreach (var r in rooms!)  // set sender name for every chat !
            {
                UserDto? user = await _userService.GetUserById(r.TenantId);
                if (user is null)
                    continue;

                ChatRoomViewModel chat = new(
                        r.Id,
                        user.Id,
                        user.FirstName + " " + user.LastName
                        );  // create new chat model
                context.ChatCollection.Replace(chat);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Rooms: OnAppearing error: " + ex.Message);
        }
    }

    // user clicks chat!
    private async void CollectionView_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
    {
        try
        {
            var context = BindingContext as RoomsPageModel;
            if (context is null)
                return;

            var selectedChat = e.CurrentSelection.FirstOrDefault() as ChatRoomViewModel;
            if (selectedChat is null)
                return;

            Debug.WriteLine($"room id: {selectedChat.RoomId}");

            UserStorage.RoomId = selectedChat.RoomId;  // store RoomId for later use !
            context.Sender = selectedChat.Title;
            context.MessageCollection.Clear();

            if (_roomService._hub is not null)  // for page reloads
                await _roomService.DisconnectWebSocket();

            await _roomService.InitSignalR(context.MessageCollection, UserStorage.RoomId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"CollectionView_SelectionChanged error: {ex.Message}");
        }
    }
}
