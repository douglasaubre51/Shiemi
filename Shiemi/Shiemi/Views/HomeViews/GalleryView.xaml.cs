using MvvmHelpers;
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

    public GalleryView()
    {
        InitializeComponent();
    }

    public ObservableRangeCollection<GalleryViewModel> Store
    {
        get => (ObservableRangeCollection<GalleryViewModel>)GetValue(StoreProperty);
        set => SetValue(StoreProperty, value);
    }
}