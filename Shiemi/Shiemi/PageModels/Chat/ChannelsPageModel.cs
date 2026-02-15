using CommunityToolkit.Mvvm.ComponentModel;

namespace Shiemi.PageModels.Chat;

public partial class ChannelsPageModel : BasePageModel
{
    [ObservableProperty]
    private string channelTitle = "Channel Title";
}
