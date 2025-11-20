using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Chat;

public partial class RoomsPageModel : BasePageModel
{
    public ObservableRangeCollection<ChatViewModel> chatCollection = [];

    public ObservableRangeCollection<MessageViewModel> messageCollection = [];

    [ObservableProperty]
    private string sender = "Sender Name";
}
