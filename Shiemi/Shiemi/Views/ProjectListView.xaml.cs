using System.Diagnostics;
using MvvmHelpers;
using Shiemi.PageModels.Chat;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Views;

public partial class ProjectListView : VerticalStackLayout
{
    public static readonly BindableProperty ProjectCollectionProperty = BindableProperty.Create(
        nameof(ProjectCollection),
        typeof(ObservableRangeCollection<ChatListProjectViewModel>),
        typeof(ProjectListView),
        new ObservableRangeCollection<ChatListProjectViewModel>(),
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            var context = bindable as ProjectListView;
            context!.ProjectCollectionView.ItemsSource =
                newvalue as ObservableRangeCollection<ChatListProjectViewModel>;
        }
        );
    public ObservableRangeCollection<ChatListProjectViewModel> ProjectCollection
    {
        get => (ObservableRangeCollection<ChatListProjectViewModel>)GetValue(ProjectCollectionProperty);
        set => SetValue(ProjectCollectionProperty, value);
    }

    private readonly ChannelClient _channelClient;

    public ProjectListView()
    {
        InitializeComponent();
        _channelClient = Provider.GetService<ChannelClient>()!;
    }

    // on project selection !
    public async void ProjectCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            ChatListProjectViewModel chat = (ChatListProjectViewModel)e.CurrentSelection.SingleOrDefault()!;
            if (chat is null)
                return;

            ChannelsPageModel context = (ChannelsPageModel)BindingContext;

            UserStorage.ChannelId = chat.ChannelId;
            context.ChannelTitle = chat.Title;
            context.MessageCollection.Clear();

            if (_channelClient._conn is not null)
            {
                await _channelClient.StopClient();
                Debug.WriteLine("restarting SignalR connection !");
            }

            await _channelClient.StartClient(context.MessageCollection, UserStorage.ChannelId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ProjectCollectionView_SelectionChanged: error: {ex.Message}");
        }
    }
}