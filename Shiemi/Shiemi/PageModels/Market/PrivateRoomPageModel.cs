using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Dtos.MessageDtos;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.PageModels.Market;

[QueryProperty(nameof(Project), nameof(Project))]
public partial class PrivateRoomPageModel : BasePageModel
{
    [ObservableProperty]
    private Project project;

    private readonly RoomService _roomService;
    private readonly UserStorage _userStorage;

    public ObservableCollection<MessageDto> MessageCollection { get; set; } = [];

    [ObservableProperty]
    private string chatBox;

    public PrivateRoomPageModel(
        RoomService roomService,
        UserStorage userStorage
        )
    {
        Title = "Private Room";
        _roomService = roomService;
        _userStorage = userStorage;
    }

    [RelayCommand]
    async Task SendChat()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ChatBox))
                return;

            MessageDto dto = new()
            {
                Text = ChatBox,
                UserId = _userStorage.UserId,
                CreatedAt = DateTime.Now.ToLocalTime(),
                RoomId = await _roomService.GetPrivateRoom(
                    _userStorage.UserId,
                    Project.Id
                    )
            };
            Debug.WriteLine($"Text: {dto.Text}");
            Debug.WriteLine($"CreatedAt: {dto.CreatedAt}");
            await _roomService.SendChat(dto);

            ChatBox = string.Empty;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"PrivateRoom SendChat error: {ex.Message}");
            await Shell.Current.DisplayAlertAsync(
                "Something went wrong!",
                "make sure u are connected to the internet!",
                "Ok"
                );
        }
    }
}
