using System.Diagnostics;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class MessageView : Grid
{

    // Messages Collection bindable prop

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
    public ObservableRangeCollection<MessageViewModel> MessageCollection
    {
        get => (ObservableRangeCollection<MessageViewModel>)GetValue(MessageCollectionProperty);
        set => SetValue(MessageCollectionProperty, value);
    }


    // sender name bindable prop

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
    public string SenderName
    {
        get => (string)GetValue(SenderNameProperty);
        set => SetValue(SenderNameProperty, value);
    }


    private readonly RoomClient? _roomService;

    public MessageView()
    {
        InitializeComponent();
        _roomService = Provider.GetService<RoomClient>();
    }


    // user clicks send btn !

    private async void Send_Btn_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(MessageBox.Text))
                return;

            var dto = new SendMessageDto(
                Text: MessageBox.Text,
                CreatedAt: DateTime.UtcNow.ToLocalTime(),
                UserId: UserStorage.UserId,
                RoomId: UserStorage.RoomId,
                ChannelId: 0  // channel id 0 since message is in room !
                );
            await _roomService!.SendChat(dto);

            MessageBox.Text = string.Empty;  // clear Entry field !
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Room: SendMessage error: {ex.Message}");
            await Shell.Current.DisplayAlertAsync(
                "Something went wrong!",
                "Couldnot send the message!",
                "Ok"
                );
        }
    }
}