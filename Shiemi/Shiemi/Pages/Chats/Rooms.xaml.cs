using Shiemi.PageModels.Chat;
using Shiemi.Services;
using System.Diagnostics;

namespace Shiemi.Pages.Chats;

public partial class Rooms : ContentPage
{
    private readonly ChatService _chatService;

    public Rooms(
        ChatService chatService,
        RoomsPageModel pageModel
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _chatService = chatService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var context = BindingContext as RoomsPageModel;
        try
        {
            var rooms = await _chatService.GetAllRooms();

            context!.RoomCollection.Clear();
            foreach (var r in rooms!)
                context!.RoomCollection.Add(r);
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
    }
}