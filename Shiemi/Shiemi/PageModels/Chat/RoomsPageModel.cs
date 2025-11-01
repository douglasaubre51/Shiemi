using Shiemi.Dtos.RoomDtos;
using System.Collections.ObjectModel;

namespace Shiemi.PageModels.Chat;

public partial class RoomsPageModel : BasePageModel
{
    public ObservableCollection<RoomDto> RoomCollection { get; set; } = [];
}
