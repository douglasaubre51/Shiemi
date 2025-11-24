using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Services.ChatServices;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;
using System.Diagnostics;

namespace Shiemi.Views;

public partial class MessageView : Grid
{
    public static readonly BindableProperty MessageCollectionProperty =
        BindableProperty.Create(
            nameof(MessageCollection),
            typeof(ObservableRangeCollection<MessageViewModel>),
            typeof(MessageView),
            new ObservableRangeCollection<MessageViewModel>(),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (MessageView)bindable;
                context.MessageCollectionView.ItemsSource =
                (ObservableRangeCollection<MessageViewModel>)newValue;

                foreach (var d in (ObservableRangeCollection<MessageViewModel>)newValue)
                {
                    Debug.WriteLine(d.Text);
                }
            });

    public static readonly BindableProperty SenderNameProperty =
        BindableProperty.Create(
            nameof(SenderName),
            typeof(string),
            typeof(MessageView),
            "Sender Name",
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (MessageView)bindable;
                context.SenderLabel.Text = (string)newValue;
            });

    public ObservableRangeCollection<MessageViewModel> MessageCollection
    {
        get => (ObservableRangeCollection<MessageViewModel>)GetValue(MessageCollectionProperty);
        set => SetValue(MessageCollectionProperty, value);
    }
    public string SenderName
    {
        get => (string)GetValue(SenderNameProperty);
        set => SetValue(SenderNameProperty, value);
    }

    private readonly RoomClient? _roomService;
    private readonly ChatService? _chatService;

    public MessageView()
    {
        InitializeComponent();
        _roomService = Provider.GetService<RoomClient>();
        _chatService = Provider.GetService<ChatService>();
    }

    private async void Send_Btn_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(MessageBox.Text))
                return;

            Debug.WriteLine("clicked send message!");
            Debug.WriteLine($"User Id: {UserStorage.UserId}");
            Debug.WriteLine($"Room Id: {UserStorage.RoomId}");

            // channelId set to 0 instead of null
            var dto = new SendMessageDto(
                Text: MessageBox.Text,
                CreatedAt: DateTime.UtcNow,
                UserId: UserStorage.UserId,
                RoomId: UserStorage.RoomId,
                ChannelId: 0
                );

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