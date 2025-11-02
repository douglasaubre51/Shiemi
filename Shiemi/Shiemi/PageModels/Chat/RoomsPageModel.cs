using CommunityToolkit.Mvvm.Input;
using Shiemi.Dtos.ChatDtos;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.PageModels.Chat;

public partial class RoomsPageModel : BasePageModel
{
    public ObservableCollection<ChatDto> ChatCollection { get; set; } = [];

    [RelayCommand]
    async Task ChatSelected(ChatDto dto)
    {
        Debug.WriteLine($"selected chat: {dto.UserName}");
    }
}
