using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Chat;

[QueryProperty(nameof(CurrentProject), "SelectedProject")]
public partial class RoomsPageModel : BasePageModel
{
    [ObservableProperty]
    private bool chatWasSelected;

    [ObservableProperty]
    private ProjectsPageProjectViewModel? currentProject;

    [ObservableProperty]
    private ObservableRangeCollection<ChatRoomViewModel> chatCollection = [];

    [ObservableProperty]
    private ObservableRangeCollection<MessageViewModel> messageCollection = [];

    [ObservableProperty]
    private string sender = "Sender Name";
}
