using Shiemi.Dtos.MessageDtos;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Shiemi.Views;

public partial class ChatView : Grid
{
    public static readonly BindableProperty MessageCollectionProperty =
        BindableProperty.Create(
            nameof(MessageCollection),
            typeof(ObservableCollection<MessageDto>),
            typeof(ChatView),
            new ObservableCollection<MessageDto>(),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (ChatView)bindable;
                context.MessageCollectionView.ItemsSource =
                (ObservableCollection<MessageDto>)newValue;

                foreach (var d in (ObservableCollection<MessageDto>)newValue)
                {
                    Debug.WriteLine(d.Text);
                }
            }
            );
    public static readonly BindableProperty SenderNameProperty =
        BindableProperty.Create(
            nameof(SenderName),
            typeof(string),
            typeof(ChatView),
            "Sender Name",
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (ChatView)bindable;
                context.SenderLabel.Text = (string)newValue;
            }
            );

    public ChatView()
    {
        InitializeComponent();
        _roomService = Provider.GetService<RoomService>();
    }

    public ObservableCollection<MessageDto> MessageCollection
    {
        get => (ObservableCollection<MessageDto>)GetValue(MessageCollectionProperty);
        set => SetValue(MessageCollectionProperty, value);
    }
    public string SenderName
    {
        get => (string)GetValue(SenderNameProperty);
        set => SetValue(SenderNameProperty, value);
    }

    private readonly RoomService? _roomService;


    private async void Send_Btn_Clicked(object sender, EventArgs e)
    {
        try
        {
            Debug.WriteLine("clicked send message!");

            if (string.IsNullOrWhiteSpace(MessageBox.Text))
                return;

            var dto = new MessageDto
            {
                Text = MessageBox.Text,
                CreatedAt = DateTime.Now,
                UserId = UserStorage.UserId,
                RoomId = UserStorage.RoomId
            };

            await _roomService!.SendChat(dto);

            // clear box
            MessageBox.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SendMessage error: {ex.Message}");
            await Shell.Current.DisplayAlertAsync(
                "Something went wrong!",
                "Couldnot send the message!",
                "Ok"
                );
        }
    }
}