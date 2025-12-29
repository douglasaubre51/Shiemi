using Microsoft.Maui.Controls.Shapes;
using MvvmHelpers;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace Shiemi.Views.DevViews;

public class ChatWidget : ContentView
{
	Editor chatBox;
	Button sendBtn;
	Label title;
	CollectionView chatCollectionView;

	public ChatWidget()
	{
		// top bar
		title = new();
		title.SetBinding(Label.TextProperty,new Binding(nameof(ChatTitle),source:this));
		var topBar = new VerticalStackLayout
		{
			Children = 
			{
				title
			},
				VerticalOptions = LayoutOptions.Start
		};
		topBar.Row(0);
		topBar.BackgroundColor = Colors.WhiteSmoke;
		topBar.Padding(4,16);

		// chat collection view
		chatCollectionView = new ()
		{
			ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
		};
		chatCollectionView.SetBinding(
				ItemsView.ItemsSourceProperty,
				new Binding(nameof(ChatCollection),source:this)
				);
		chatCollectionView.ItemTemplate = new DataTemplate(() =>
				{
				Label text = new ();
				text.FontSize = 16;
				text.TextColor = Colors.White;
				text.SetBinding(Label.TextProperty,static (Chat chat) => chat.Text);

				Label dateTime = new ();
				dateTime.FontSize = 10;
				dateTime.TextColor = Colors.WhiteSmoke;
				dateTime.SetBinding(Label.TextProperty,static (Chat chat) => chat.SentAt);

				VerticalStackLayout cardLayout = new ();
				cardLayout.Padding(4,8); 
				cardLayout.Add(text);
				cardLayout.Add(dateTime);

				Border border = new ();
				border.HorizontalOptions = LayoutOptions.End;
				border.BackgroundColor = Colors.LightGreen;
				border.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(4,4,4,0) };
				border.StrokeThickness = 0;
				border.Content = cardLayout;

				VerticalStackLayout layout = new ();
				layout.Padding(10,4);

				layout.Add(border);

				return layout;
				});
		chatCollectionView.Row(1);

		// bottom bar
		chatBox = new()
		{
			HeightRequest = 40,
						  MinimumWidthRequest = 300,
						  HorizontalOptions = LayoutOptions.Center
		};
		sendBtn = new()
		{
			Text = "send",
				 HorizontalOptions = LayoutOptions.End
		};
		sendBtn.Clicked += SendBtn_Clicked!;

		var bottomBar = new HorizontalStackLayout
		{
			Children =
			{
				chatBox,
				sendBtn
			},
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.Center,
				Spacing = 4
		};
		bottomBar.Row(2);

		// root layout
		Grid mainGrid = new ();
		mainGrid.RowSpacing = 8;
		mainGrid.RowDefinitions = Rows.Define(
				100,
				Star,
				Auto
				);
		mainGrid.Add(topBar);
		mainGrid.Add(chatCollectionView);
		mainGrid.Add(bottomBar);

		Content = mainGrid;
	}

	private void SendBtn_Clicked(object sender, EventArgs e)
	{
		if(string.IsNullOrWhiteSpace(chatBox.Text)) return;

		Chat chat = new (chatBox.Text,DateTime.UtcNow.ToLocalTime());
		ChatCollection.Add(chat);

		DidSendChat = true;
		chatBox.Text = string.Empty;
	}

	public static BindableProperty DidSendChatProperty = BindableProperty.Create(
			nameof(DidSendChat),
			typeof(bool),
			typeof(ChatWidget),
			false);
	public bool DidSendChat
	{
		get => (bool) GetValue(DidSendChatProperty);
		set => SetValue(DidSendChatProperty,value);
	}

	public static BindableProperty ChatTitleProperty = BindableProperty.Create(
			nameof(ChatTitle),
			typeof(string),
			typeof(ChatWidget),
			"chat title");
	public string ChatTitle
	{
		get => (string)GetValue(ChatTitleProperty);
		set => SetValue(ChatTitleProperty, value);
	}

	public static BindableProperty ChatCollectionProperty = BindableProperty.Create(
			nameof(ChatCollection),
			typeof(ObservableRangeCollection<Chat>),
			typeof(ChatWidget),
			new ObservableRangeCollection<Chat>());
	public ObservableRangeCollection<Chat> ChatCollection
	{
		get => (ObservableRangeCollection<Chat>)GetValue(ChatCollectionProperty);
		set => SetValue(ChatCollectionProperty, value);
	}

}

public record Chat(
		string Text,
		DateTime SentAt
		);
