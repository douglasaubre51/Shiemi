using CommunityToolkit.Mvvm.ComponentModel;
using Shiemi.Dtos.ChatDtos;
using Shiemi.Dtos.MessageDtos;
using System.Collections.ObjectModel;

namespace Shiemi.PageModels.Chat;

public partial class RoomsPageModel : BasePageModel
{
    [ObservableProperty]
    private ObservableCollection<ChatDto> chatCollection = [];

    [ObservableProperty]
    private ObservableCollection<MessageDto> messageCollection = [];

    [ObservableProperty]
    private string sender = "Sender Name";
}
