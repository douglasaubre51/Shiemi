using Shiemi.PageModels.Chat;

namespace Shiemi.Pages.Chats;

public partial class PrivateRoomChats : ContentPage
{
    public PrivateRoomChats(PrivateRoomChatsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as PrivateRoomChatsPageModel;
        context!.IsPageLoading = true;
    }
}