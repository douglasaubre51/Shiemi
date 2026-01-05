using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.User;

[QueryProperty(nameof(ProjectCollection), "Projects")]
public partial class ViewMoreProjectsPageModel : BasePageModel
{
    [ObservableProperty]
    private ObservableRangeCollection<GalleryViewModel> projectCollection = [];
}
