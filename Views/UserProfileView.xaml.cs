using Shiemi.Services;

namespace Shiemi.Views;

public partial class UserProfileView : ContentPage
{
    private readonly StorageService _storageService;
    public UserProfileView(StorageService storageService)
    {
        InitializeComponent();
        _storageService = storageService;

        _storageService.ViewUserDetails();
    }
}