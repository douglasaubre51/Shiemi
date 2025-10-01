using CommunityToolkit.Mvvm.ComponentModel;

namespace Shiemi.PageModels;

public partial class BasePageModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;
    public bool IsNotBusy => !IsBusy;
}
