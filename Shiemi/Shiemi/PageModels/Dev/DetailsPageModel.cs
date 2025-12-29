using CommunityToolkit.Mvvm.ComponentModel;
using Shiemi.Models;

namespace Shiemi.PageModels.Dev;

[QueryProperty(nameof(CurrentDev), "CurrentDev")]
public partial class DetailsPageModel : BasePageModel
{
    [ObservableProperty]
    private DevModel? currentDev;
}
