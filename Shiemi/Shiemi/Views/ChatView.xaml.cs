using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Services.ChatServices;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using Shiemi.Utilities.ServiceProviders;
using System.Diagnostics;

namespace Shiemi.Views;

public partial class ChatView : Grid
{
    public static readonly BindableProperty MessageCollectionProperty =
        BindableProperty.Create(
            nameof(MessageCollection),
            typeof(ObservableRangeCollection<MessageDto>),
            typeof(ChatView),
            new ObservableRangeCollection<MessageDto>(),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var context = (ChatView)bindable;
                context.MessageCollectionView.ItemsSource =
                (ObservableRangeCollection<MessageDto>)newValue;

                foreach (var d in (ObservableRangeCollection<MessageDto>)newValue)
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

    public ObservableRangeCollection<MessageDto> MessageCollection
    {
        get => (ObservableRangeCollection<MessageDto>)GetValue(MessageCollectionProperty);
        set => SetValue(MessageCollectionProperty, value);
    }
    public string SenderName
    {
        get => (string)GetValue(SenderNameProperty);
        set => SetValue(SenderNameProperty, value);
    }

    private readonly RoomClient? _roomService;
    private readonly ChatService? _chatService;

    public ChatView()
    {
        InitializeComponent();
        _roomService = Provider.GetService<RoomClient>();
        _chatService = Provider.GetService<ChatService>();

        // on pull to refresh!
        //MessageRefreshView.Command = new Command(() =>
        //{
        //    List<MessageDto>? messages = _chatService!.GetAllByRoom(UserStorage.RoomId)
        //        .GetAwaiter().GetResult();
        //    if (messages is null)
        //    {
        //        MessageRefreshView.IsRefreshing = false;
        //        return;
        //    }

        //    MessageCollection.ReplaceRange(messages);
        //    MessageRefreshView.IsRefreshing = false;
        //});
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