using Shiemi.ViewModels;
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
				text.SetBinding(Label.TextProperty,static (MessageViewModel chat) => chat.Text);

				Label dateTime = new ();
				dateTime.FontSize = 10;
				dateTime.TextColor = Colors.WhiteSmoke;
				dateTime.SetBinding(Label.TextProperty,static (MessageViewModel chat) => chat.CreatedAt);

				VerticalStackLayout cardLayout = new ();
				cardLayout.Padding(4,8); 
				cardLayout.Add(text);
				cardLayout.Add(dateTime);

				Border border = new();
				border.SetBinding(
					Border.HorizontalOptionsProperty,
					new Binding(nameof(MessageViewModel.IsOwner))
					{
						Converter = new FuncConverter<bool,LayoutOptions>(isOwner =>
							isOwner ? LayoutOptions.End : LayoutOptions.Start)
					});

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
		chatBox.SetBinding(Editor.TextProperty, new Binding(nameof(ChatText), source: this));
		chatBox.SetBinding(Editor.IsEnabledProperty, new Binding(nameof(IsNotLoggedInUser), source: this));

		sendBtn = new()
		{
			Text = "send",
				 HorizontalOptions = LayoutOptions.End
		};
		sendBtn.Clicked += SendBtn_Clicked!;
		sendBtn.SetBinding(Button.IsEnabledProperty, new Binding(nameof(IsNotLoggedInUser), source: this));

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

		DidSendChat = true;
		chatBox.Text = string.Empty;
	}

	public static BindableProperty IsNotLoggedInUserProperty = BindableProperty.Create(
			nameof(IsNotLoggedInUser),
			typeof(bool),
			typeof(ChatWidget),
			false);
	public bool IsNotLoggedInUser
	{
		get => (bool) GetValue(IsNotLoggedInUserProperty);
		set => SetValue(IsNotLoggedInUserProperty,value);
	}

	public static BindableProperty ChatTextProperty = BindableProperty.Create(
			nameof(ChatText),
			typeof(string),
			typeof(ChatWidget),
			string.Empty);
	public string ChatText
	{
		get => (string) GetValue(ChatTextProperty);
		set => SetValue(ChatTextProperty,value);
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
			typeof(ObservableRangeCollection<MessageViewModel>),
			typeof(ChatWidget),
			new ObservableRangeCollection<MessageViewModel>());
	public ObservableRangeCollection<MessageViewModel> ChatCollection
	{
		get => (ObservableRangeCollection<MessageViewModel>)GetValue(ChatCollectionProperty);
		set => SetValue(ChatCollectionProperty, value);
	}
}
