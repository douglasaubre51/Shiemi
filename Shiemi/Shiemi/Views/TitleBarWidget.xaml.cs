namespace Shiemi.Views;

public partial class TitleBarWidget : TitleBar
{
	public TitleBarWidget()
		=> InitializeComponent();

	public bool SearchBarIsEnabled
	{
		get => (bool) GetValue(SearchBarIsEnabledProperty);
		set => SetValue(SearchBarIsEnabledProperty, value);
	}
	public static readonly BindableProperty SearchBarIsEnabledProperty = BindableProperty.Create(
		nameof(SearchBarIsEnabled),
		typeof(bool),
		typeof(TitleBarWidget),
		false,
		propertyChanged:(bindable, oldValue, newValue) =>
		{
			var context = bindable as TitleBarWidget;
			context!.SearchBarView.IsVisible = (bool) newValue;
		}
		);
}