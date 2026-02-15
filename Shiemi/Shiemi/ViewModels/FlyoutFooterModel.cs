using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Shiemi.ViewModels;

public partial class FlyoutFooterModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string profile = string.Empty;

    [ObservableProperty]
    private string emailId = string.Empty;

    [ObservableProperty]
    private string role = string.Empty;

    [RelayCommand]
    async Task GoToProfile()
        => await Shell.Current.GoToAsync(
            "///Profile",
            true);
}
