using CommunityToolkit.Mvvm.Input;
using Shiemi.Models;

namespace Shiemi.PageModels.Dev;

public partial class MarketpageModel : BasePageModel
{
    [RelayCommand]
    async Task GoToDetailsPage(DevModel selectedDevModel)
        => await Shell.Current.GoToAsync(
            "DetailsDev",
            true,
            new Dictionary<string, object>()
            {
                {"CurrentDev",selectedDevModel }
            });
}
