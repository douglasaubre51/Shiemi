using AutoMapper;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Services;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Views.HomeViews;

public partial class GalleryView : CollectionView
{
    public static BindableProperty StoreProperty = BindableProperty.Create(
        nameof(Store),
        typeof(ObservableRangeCollection<GalleryViewModel>),
        typeof(GalleryView),
        new ObservableRangeCollection<GalleryViewModel>(),
        propertyChanged: (bindable, value, newValue) =>
        {
            var context = bindable as GalleryView;
            context!.GalleryCollectionView.ItemsSource =
                newValue as ObservableRangeCollection<GalleryViewModel>;
        }
    );

    private readonly ProjectService _projectServ;

    public GalleryView()
    {
        InitializeComponent();
        _projectServ = Provider.GetService<ProjectService>()!;
    }

    public ObservableRangeCollection<GalleryViewModel> Store
    {
        get => (ObservableRangeCollection<GalleryViewModel>)GetValue(StoreProperty);
        set => SetValue(StoreProperty, value);
    }

    private async Task GalleryCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }
}