using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Services;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Chat;

[QueryProperty(nameof(CurrentProject), "SelectedProject")]
public partial class PrivateRoomChatsPageModel(
    ChatService chatServ,
    UserService userServ) : BasePageModel
{
    private readonly ChatService _chatServ = chatServ;
    private readonly UserService _userServ = userServ;

    [ObservableProperty]
    private ProjectsPageProjectViewModel? currentProject;

    [ObservableProperty]
    private ObservableRangeCollection<PrivateChatRoomViewModel> chatRoomsList = [];

    [ObservableProperty]
    private bool isPageLoading;


    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            var rooms = await _chatServ.GetAllRooms(CurrentProject!.Id);
            if (rooms is null || rooms.Count is 0) return;

            List<PrivateChatRoomViewModel> chatRooms = [];

            foreach (var room in rooms)
            {
                UserDto? user = await _userServ.GetUserById(room.TenantId);
                if (user is null) continue;

                chatRooms.Add(new PrivateChatRoomViewModel(
                    RoomId: room.Id,
                    SenderId: room.TenantId,
                    Title: user.FirstName + " " + user.LastName,
                    Profile: user.ProfilePhotoURL
                    )
                );
            }

            ChatRoomsList.AddRange(chatRooms);
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
