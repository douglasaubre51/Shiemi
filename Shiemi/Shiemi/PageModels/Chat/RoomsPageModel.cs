using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.Dtos.ChatDtos;
using Shiemi.Dtos.MessageDtos;

namespace Shiemi.PageModels.Chat;

public partial class RoomsPageModel : BasePageModel
{
    [ObservableProperty]
    private ObservableRangeCollection<ChatDto> chatCollection = [];

    [ObservableProperty]
    private ObservableRangeCollection<MessageDto> messageCollection = [];

    [ObservableProperty]
    private string sender = "Sender Name";
}
