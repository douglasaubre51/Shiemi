using Shiemi.PageModels.Dev;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;

namespace Shiemi.Pages.Dev;

public partial class Chat : ContentPage
{
    private readonly RoomClient _roomClient;

    public Chat(
        ChatPageModel pageModel,
        RoomClient roomClient)
    {
        InitializeComponent();
        BindingContext = pageModel;
        _roomClient = roomClient;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as ChatPageModel;
        if (context is null)
        {
            await Shell.Current.GoToAsync("DetailsDev");
            return;
        }

        try
        {
            await _roomClient.InitSignalR(
                context.Messages,
                context.CurrentClient.RoomId,
                UserStorage.UserId,
                RoomTypes.DEV
                );

            UserStorage.RoomId = context.CurrentClient.RoomId;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}