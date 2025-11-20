using Shiemi.Dtos;
using Shiemi.PageModels.Chat;
using Shiemi.Services;
using Shiemi.Services.ChatServices;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
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
        var context = BindingContext as RoomsPageModel;
        try
        {
            var rooms = await _chatService.GetAllRooms();
            if (rooms is null)
            {
                Debug.WriteLine("GetAllRooms: null!");
                return;
            }

            // clear collection!
            context!.ChatCollection.Clear();
            foreach (var r in rooms!)
            {
                AccountDto? user = await _userService.GetUserById(r.TenantId);
                if (user is null)
                {
                    Debug.WriteLine($"User:{r.TenantId} is null!");
                    return;
                }
                // create chat
                ChatDto chat = new ChatDto
                {
                    UserName = user.FirstName + " " + user.LastName,
                    RoomId = r.Id,
                    Profile = user.Profile
                };
                context.ChatCollection.Add(chat);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        finally { base.OnAppearing(); }
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            ChatDto selectedChat = (ChatDto)e.CurrentSelection.FirstOrDefault();
            if (selectedChat is null)
                return;

            var context = BindingContext as RoomsPageModel;

            ChatPageModel model = new()
            {
                RoomId = selectedChat.RoomId,
                SenderName = selectedChat.UserName,
                Profile = selectedChat.Profile,
                MessageCollection = context!.MessageCollection
            };


            // set room id for messages
            UserStorage.RoomId = model.RoomId;
            Debug.WriteLine($"RoomId: {model.RoomId}");
            // set senderName prop
            context.Sender = model.SenderName;
            // set messageCollection prop
            context.MessageCollection.Clear();
            context.MessageCollection = model.MessageCollection;

            Debug.WriteLine($"sender name: {context.Sender}");

            // remove existing socket conn!
            if (_roomService._hub is not null)
                await _roomService.DisconnectWebSocket();

            await _roomService.InitSignalR(model.MessageCollection, model.RoomId);
        }
        catch (Exception ex)
        { Debug.WriteLine($"CollectionView_SelectionChanged error: {ex.Message}"); }
    }
}