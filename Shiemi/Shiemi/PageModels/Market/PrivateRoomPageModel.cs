using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.Dtos.MessageDtos;
using Shiemi.Models;

namespace Shiemi.PageModels.Market;

[QueryProperty(nameof(Project), nameof(Project))]
public partial class PrivateRoomPageModel : BasePageModel
{
    [ObservableProperty]
    private Project project;

    public ObservableRangeCollection<MessageDto> MessageCollection { get; set; } = [];

    [ObservableProperty]
    private string chatBox;
    [ObservableProperty]
    private string sender;

    public PrivateRoomPageModel() => Title = "Private Room";

}
