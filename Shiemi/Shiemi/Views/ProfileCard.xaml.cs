namespace Shiemi.Views;

public partial class ProfileCard : ContentView
{
	public ProfileCard()
		=> InitializeComponent();

	public int UserId
	{
		get => (int)GetValue(UserIdProperty);
		set => SetValue(UserIdProperty, value);
	}
	public static readonly BindableProperty UserIdProperty = BindableProperty.Create(
		nameof(UserId),
		typeof(int),
		typeof(ProfileCard));
	public string ProfilePhoto
	{
		get => (string)GetValue(ProfilePhotoProperty);
		set => SetValue(ProfilePhotoProperty, value);
	}
	public static readonly BindableProperty ProfilePhotoProperty = BindableProperty.Create(
		nameof(ProfilePhoto),
		typeof(string),
		typeof(ProfileCard),
		propertyChanged: (binding, oldValue, newValue) =>
		{
			var context = binding as ProfileCard;
			context!.ProfileImage.Source = newValue as string;
		});
	public string Username
	{
		get => (string)GetValue(UsernameProperty);
		set => SetValue(UsernameProperty, value);
	}
	public static readonly BindableProperty UsernameProperty = BindableProperty.Create(
		nameof(Username),
		typeof(string),
		typeof(ProfileCard),
		propertyChanged: (binding, oldValue, newValue) =>
		{
			var context = binding as ProfileCard;
			context!.UsernameLabel.Text = newValue as string;
		});
}