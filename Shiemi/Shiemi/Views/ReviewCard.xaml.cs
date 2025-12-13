using MvvmHelpers;
using Shiemi.Models;

namespace Shiemi.Views;

public partial class ReviewCard : Border
{
    public BindableProperty ReviewCollectionProperty = BindableProperty.Create(
        nameof(ReviewCollection),
        typeof(ObservableRangeCollection<Review>),
        typeof(ReviewCard),
        new ObservableRangeCollection<Review>(),
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {

        }
        );
    public ObservableRangeCollection<Review> ReviewCollection
    {
        get => (ObservableRangeCollection<Review>)GetValue(ReviewCollectionProperty);
        set => SetValue(ReviewCollectionProperty, value);
    }

    public ReviewCard()
    {
        InitializeComponent();
    }
}