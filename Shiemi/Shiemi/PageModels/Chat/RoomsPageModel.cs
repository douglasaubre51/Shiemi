using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Chat;

public partial class RoomsPageModel : BasePageModel
{
    [ObservableProperty]
    private ObservableRangeCollection<ChatViewModel> chatCollection = [];

    [ObservableProperty]
    private ObservableRangeCollection<MessageViewModel> messageCollection = [];

    [ObservableProperty]
    private string sender = "Sender Name";
}
