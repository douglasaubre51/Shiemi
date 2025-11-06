using Shiemi.Dtos.MessageDtos;
using System.Collections.ObjectModel;

namespace Shiemi.PageModels.Chat;

public partial class ChatPageModel : BasePageModel
{
    public int RoomId { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string? Profile { get; set; }

    public ObservableCollection<MessageDto> MessageCollection { get; set; } = [];
}
