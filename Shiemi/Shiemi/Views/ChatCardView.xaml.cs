namespace Shiemi.Views;

public partial class ChatCardView : VerticalStackLayout
{
    public static readonly BindableProperty MessageTextProperty =
        BindableProperty.Create(
            nameof(MessageText),
            typeof(string),
            typeof(ChatCardView),
            "loading message!",
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (ChatCardView)bindable;
                context.MessageTextLabel.Text = (string)newValue;
            }
            );
    public static readonly BindableProperty MessageTimeProperty =
        BindableProperty.Create(
            nameof(MessageTime),
            typeof(string),
            typeof(ChatCardView),
            "loading time!",
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (ChatCardView)bindable;
                context.MessageTimeLabel.Text = (string)newValue;
            }
            );

    public ChatCardView()
    {
        InitializeComponent();
    }

    public string MessageText
    {
        get => (string)GetValue(MessageTextProperty);
        set => SetValue(MessageTextProperty, value);
    }
    public string MessageTime
    {
        get => (string)GetValue(MessageTimeProperty);
        set => SetValue(MessageTimeProperty, value);
    }
}