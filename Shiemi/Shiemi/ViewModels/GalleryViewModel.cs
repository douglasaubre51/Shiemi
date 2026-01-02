namespace Shiemi.ViewModels;

public record GalleryViewModel(
    int ItemId,
    string ThumbnailUrl,
    string CoverTitle,
    string CoverBlurb
);