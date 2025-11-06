using MvvmHelpers;
using Shiemi.Dtos.MessageDtos;

namespace Shiemi.PageModels.Chat;

public partial class ChatPageModel : BasePageModel
{
    public int RoomId { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string? Profile { get; set; }

    public ObservableRangeCollection<MessageDto> MessageCollection { get; set; } = [];
}
