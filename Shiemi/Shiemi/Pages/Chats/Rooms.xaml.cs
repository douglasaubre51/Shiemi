using Shiemi.Dtos.ChatDtos;
using Shiemi.Dtos.UserDtos;
using Shiemi.PageModels.Chat;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.Pages.Chats;

public partial class Rooms : ContentPage
{
    private readonly ChatService _chatService;
    private readonly UserService _userService;

    public Rooms(
        ChatService chatService,
        RoomsPageModel pageModel,
        UserService userService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _chatService = chatService;
        _userService = userService;
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

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ChatDto selectedChat = (ChatDto)e.CurrentSelection.FirstOrDefault();
        if (selectedChat is null)
            return;

        Debug.WriteLine(selectedChat!.UserName);
    }
}