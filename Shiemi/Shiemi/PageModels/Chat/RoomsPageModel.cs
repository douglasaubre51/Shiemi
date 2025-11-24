using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Chat;

public partial class RoomsPageModel : BasePageModel
{
    public ObservableRangeCollection<ChatViewModel> ChatCollection = [];

    public ObservableRangeCollection<MessageViewModel> MessageCollection = [];

    [ObservableProperty]
    private string sender = "Sender Name";
}
