using CommunityToolkit.Mvvm.ComponentModel;
using Shiemi.Models;

namespace Shiemi.PageModels.Dev;

[QueryProperty(nameof(CurrentClient), "CurrentClient")]
public partial class ChatPageModel : BasePageModel
{
    [ObservableProperty]
    private ProfileCardModel currentClient;

}
