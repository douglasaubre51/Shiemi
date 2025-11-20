using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Chat;

public partial class ChannelsPageModel : BasePageModel
{
    public ObservableRangeCollection<ChatListProjectVM> ProjectCollection { get; set; } = [];
    public ObservableRangeCollection<MessageViewModel> MessageCollection { get; set; } = [];
}
