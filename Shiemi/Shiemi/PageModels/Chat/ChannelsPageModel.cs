using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Chat;

public partial class ChannelsPageModel : BasePageModel
{
    [ObservableProperty]
    private ObservableRangeCollection<ChatListProjectViewModel> projectCollection = [];
    [ObservableProperty]
    private ObservableRangeCollection<MessageViewModel> messageCollection = [];

    [ObservableProperty]
    private string channelTitle = "Channel Title";
}
