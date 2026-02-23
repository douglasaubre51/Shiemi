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
    private readonly UserService _userService;

    public ReviewCard()
    {
        InitializeComponent();
        _reviewService = Provider.GetService<ReviewService>()!;
        _userService = Provider.GetService<UserService>()!;
    }

    private async void CreateReviewButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ReviewEditorView.Text))
            return;

        try
        {
            CreateReviewButton.IsEnabled = false;

            // Check if review is allowed !
            var flag = await _userService.CheckIfReviewIsAllowed(UserStorage.UserId, CurrentProjectId);
            if(flag is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Invalid attempt",
                    "Users who are not on this project can't write a review!",
                    "Ok");
                return;
            }

            Review review = new()
            {
                UserId = UserStorage.UserId,
                ProjectId = CurrentProjectId,
                Text = ReviewEditorView.Text,
                CreatedAt = DateTime.UtcNow
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
        finally
        {
            CreateReviewButton.IsEnabled = true;
        }
    }
}