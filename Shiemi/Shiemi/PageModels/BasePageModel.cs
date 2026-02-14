using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Shiemi.PageModels;

public partial class BasePageModel : ObservableObject
{

    [ObservableProperty]
    private string searchText;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    private string title = string.Empty;

    public BasePageModel()
    {
        SearchText = "hey !";
    }

    [RelayCommand]
    async Task SearchSelectedItem()
    {
        Debug.WriteLine("search btn pressed!");
        Debug.WriteLine(SearchText);
    }
}
