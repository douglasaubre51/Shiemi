namespace Shiemi.ViewModels;

public record GalleryViewModel(
    int ItemId,
    string CoverTitle,
    string CoverBlurb,
    string CoverPhoto = ""
);