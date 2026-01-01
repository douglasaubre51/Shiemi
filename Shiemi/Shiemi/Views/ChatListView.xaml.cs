using MvvmHelpers;
using Shiemi.Dtos;

namespace Shiemi.Views;

public partial class ChatListView : VerticalStackLayout
{
    public static readonly BindableProperty ChatCollectionProperty = BindableProperty.Create(
            nameof(ChatCollection),
            typeof(ObservableRangeCollection<ChatDto>),
            typeof(ChatListView),
            new ObservableRangeCollection<ChatDto>(),
            propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                var context = bindable as ChatListView;
                context!.ChatCollectionView.ItemsSource = newvalue as ObservableRangeCollection<ChatDto>;
            }
            );

    public ObservableRangeCollection<ChatDto> ChatCollection
    {
        get => (ObservableRangeCollection<ChatDto>)GetValue(ChatCollectionProperty);
        set => SetValue(ChatCollectionProperty, value);
    }

    public ChatListView()
        => InitializeComponent();
}
