using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Market;

[QueryProperty(nameof(CurrentProjectVM), "ProjectVM")]
public partial class PrivateRoomPageModel : BasePageModel
{
    [ObservableProperty]
    private ProjectViewModel currentProjectVM;

    public ObservableRangeCollection<MessageViewModel> MessageCollection { get; set; } = [];

    [ObservableProperty]
    private string chatBox;
    [ObservableProperty]
    private string sender;

    public PrivateRoomPageModel() => Title = "Private Room";
}
