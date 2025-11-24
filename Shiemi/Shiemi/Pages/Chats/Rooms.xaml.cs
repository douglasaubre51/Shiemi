using Shiemi.Dtos;
using Shiemi.PageModels.Chat;
using Shiemi.Services;
using Shiemi.Services.ChatServices;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using Shiemi.ViewModels;
using System.Diagnostics;

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

    protected override async void OnAppearing()
    {
        try
        {
            var context = BindingContext as RoomsPageModel;
            if (context is null)
            {
                Debug.WriteLine($"RoomsPageModel context is null!");
                return;
            }

            // fetch chatlist data
            var rooms = await _chatService.GetAllRooms();
            if (rooms is null)
            {
                Debug.WriteLine("GetAllRooms: null!");
                return;
            }

            // clear collection before flush!
            context.ChatCollection.Clear();

            // set sendername for each room!
            foreach (var r in rooms!)
            {
                UserDto? user = await _userService.GetUserById(r.TenantId);
                if (user is null)
                {
                    Debug.WriteLine($"User:{r.TenantId} is null!");
                    continue;
                }

                // create and add room
                ChatViewModel chat = new(user.Id, user.FirstName + " " + user.LastName);
                context.ChatCollection.Add(chat);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Rooms: OnAppearing error: " + ex);
        }
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var selectedChat = (ChatViewModel)e.CurrentSelection.FirstOrDefault();
            if (selectedChat is null)
            {
                Debug.WriteLine($"selected chat is null!");
                return;
            }

            var context = BindingContext as RoomsPageModel;
            if (context is null)
            {
                Debug.WriteLine($"RoomsPageModel context is null!");
                return;
            }

            // set room id for loading messages!
            UserStorage.RoomId = selectedChat.Id;
            Debug.WriteLine($"RoomId: {selectedChat.Id}");

            // set senderName in pagemodel
            context.Sender = selectedChat.Title;
            Debug.WriteLine($"sender name: {context.Sender}");

            // clear messageCollection before flush
            context.MessageCollection.Clear();

            // remove existing socket conn before reconnection!
            if (_roomService._hub is not null)
                await _roomService.DisconnectWebSocket();
            await _roomService.InitSignalR(context.MessageCollection, UserStorage.RoomId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"CollectionView_SelectionChanged error: {ex.Message}");
        }
    }
}