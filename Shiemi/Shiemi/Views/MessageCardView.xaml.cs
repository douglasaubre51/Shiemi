namespace Shiemi.Views;

public partial class MessageCardView : Border
{
    public static readonly BindableProperty MessageTextProperty = BindableProperty.Create(
            nameof(MessageText),
            typeof(string),
            typeof(MessageCardView),
            "loading message!",
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (MessageCardView)bindable;
                context.MessageTextLabel.Text = (string)newValue;
            }
            );
    public string MessageText
    {
        get => (string)GetValue(MessageTextProperty);
        set => SetValue(MessageTextProperty, value);
    }
    public static readonly BindableProperty MessageTimeProperty = BindableProperty.Create(
            nameof(MessageTime),
            typeof(string),
            typeof(MessageCardView),
            "loading time!",
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (MessageCardView)bindable;
                context.MessageTimeLabel.Text = (string)newValue;
            }
            );
    public string MessageTime
    {
        get => (string)GetValue(MessageTimeProperty);
        set => SetValue(MessageTimeProperty, value);
    }

    public MessageCardView()
        => InitializeComponent();
}