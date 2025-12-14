using MvvmHelpers;
using Shiemi.Models;
using System.Diagnostics;

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

            foreach (var r in context.ReviewCollection)
                Debug.WriteLine("review: " + r.Text);
        }
        );
    public ObservableRangeCollection<Review> ReviewCollection
    {
        get => (ObservableRangeCollection<Review>)GetValue(ReviewCollectionProperty);
        set => SetValue(ReviewCollectionProperty, value);
    }

    public static readonly BindableProperty AllowedToWriteProperty = BindableProperty.Create(
        nameof(AllowedToWrite),
        typeof(bool),
        typeof(ReviewCard),
        false,
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            var context = (ReviewCard)bindable;
            context.CreateReviewButton.IsEnabled = (bool)newvalue;
        }
        );
    public bool AllowedToWrite
    {
        get => (bool)GetValue(AllowedToWriteProperty);
        set => SetValue(AllowedToWriteProperty, value);
    }

    public ReviewCard()
        => InitializeComponent();
}