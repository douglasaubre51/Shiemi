using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.User;

public partial class HomePageModel : BasePageModel
{
    [ObservableProperty] private ObservableRangeCollection<GalleryViewModel> joinedProjectCollection = [];
    [ObservableProperty] private ObservableRangeCollection<GalleryViewModel> myProjectCollection = [];
}