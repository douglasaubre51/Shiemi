using System.Diagnostics;
using MvvmHelpers;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;

namespace Shiemi.Views;

public partial class ReviewCard : Border
{
    public static readonly BindableProperty ReviewCollectionProperty = BindableProperty.Create(
        nameof(ReviewCollection),
        typeof(ObservableRangeCollection<Review>),
        typeof(ReviewCard),
        new ObservableRangeCollection<Review>(),
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            var context = (ReviewCard)bindable;
            context.ReviewCollectionView.ItemsSource =
                (ObservableRangeCollection<Review>)newvalue;
        }
        );
    public ObservableRangeCollection<Review> ReviewCollection
    {
        get => (ObservableRangeCollection<Review>)GetValue(ReviewCollectionProperty);
        set => SetValue(ReviewCollectionProperty, value);
    }

    public static readonly BindableProperty NotAllowedToWriteProperty = BindableProperty.Create(
        nameof(NotAllowedToWrite),
        typeof(bool),
        typeof(ReviewCard),
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            var context = (ReviewCard)bindable;
            context.CreateReviewButton.IsVisible = !(bool)newvalue;
            Debug.WriteLine($"allowed to write: {(bool)newvalue}");
        }
        );
    public bool NotAllowedToWrite
    {
        get => (bool)GetValue(NotAllowedToWriteProperty);
        set => SetValue(NotAllowedToWriteProperty, value);
    }

    public static readonly BindableProperty CurrentProjectIdProperty = BindableProperty.Create(
        nameof(CurrentProjectId),
        typeof(int),
        typeof(ReviewCard)
        );
    public int CurrentProjectId
    {
        get => (int)GetValue(CurrentProjectIdProperty);
        set => SetValue(CurrentProjectIdProperty, value);
    }

    private readonly ReviewService _reviewService;

    public ReviewCard()
    {
        InitializeComponent();
        _reviewService = Provider.GetService<ReviewService>()!;
    }

    private async void CreateReviewButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ReviewEditorView.Text))
            return;

        try
        {
            Review review = new()
            {
                UserId = UserStorage.UserId,
                ProjectId = CurrentProjectId,
                Text = ReviewEditorView.Text,
                CreatedAt = DateTime.UtcNow.ToLocalTime()
            };
            bool result = await _reviewService.CreateReview(review);
            if (result is false)
                await Shell.Current.DisplayAlertAsync(
                    "Failure",
                    "Couldn't create new review !",
                    "Ok");

            ReviewEditorView.Text = string.Empty;
            ReviewCollection.Add(review);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"CreateReview: error: {ex.Message}");
        }
    }
}