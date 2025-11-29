using CommunityToolkit.Mvvm.ComponentModel;

namespace Shiemi.PageModels.User;

public partial class ProfilePageModel : BasePageModel
{
    [ObservableProperty]
    private string firstName = string.Empty;
    [ObservableProperty]
    private string lastName = string.Empty;
    [ObservableProperty]
    private string userName = string.Empty;
    [ObservableProperty]
    private string email = string.Empty;
    [ObservableProperty]
    private string userId = string.Empty;

}
