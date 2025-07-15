using Shiemi.Services;
using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class UserProfileView : ContentPage
{
    private readonly StorageService _storageService;
    public UserProfileView(StorageService storageService, UserProfileVM viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        _storageService = storageService;
    }
}