using System.Diagnostics;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class ChannelMessageView : Grid
{
	// Messages Collection bindable prop
	public static readonly BindableProperty MessageCollectionProperty =
		BindableProperty.Create(
			nameof(MessageCollection),
			typeof(ObservableRangeCollection<MessageViewModel>),
			typeof(ChannelMessageView),
			new ObservableRangeCollection<MessageViewModel>(),
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				var context = (ChannelMessageView)bindable;
				context.MessageCollectionView.ItemsSource =
				(ObservableRangeCollection<MessageViewModel>)newValue;

				foreach (var d in (ObservableRangeCollection<MessageViewModel>)newValue)
					Debug.WriteLine(d.Text);
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
			typeof(ChannelMessageView),
			"Sender Name",
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				var context = (ChannelMessageView)bindable;
				context.SenderLabel.Text = (string)newValue;
			});
	public string SenderName
	{
		get => (string)GetValue(SenderNameProperty);
		set => SetValue(SenderNameProperty, value);
	}

	private readonly ChannelClient? _channelClient;

	public ChannelMessageView()
	{
		InitializeComponent();
		_channelClient = Provider.GetService<ChannelClient>();

		Unloaded += (s, e) => MessageCollection.Clear();
	}

	// user clicks send btn !
	private async void Send_Btn_Clicked(object sender, EventArgs e)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(ChannelMessageBox.Text))
				return;

			var dto = new SendMessageDto(
				Text: ChannelMessageBox.Text,
				CreatedAt: DateTime.UtcNow.ToLocalTime(),
				UserId: UserStorage.UserId,
				RoomId: 0,  // default the roomId !
				ChannelId: UserStorage.ChannelId
			);
			await _channelClient!.SendChat(dto);

			ChannelMessageBox.Text = string.Empty;  // clear Entry field !
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Channel: SendMessage error: {ex.Message}");
			await Shell.Current.DisplayAlertAsync(
				"Something went wrong!",
				"Couldnot send the message!",
				"Ok"
			);
		}
	}
}