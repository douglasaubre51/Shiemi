using MvvmHelpers;
using Shiemi.ViewModels;
using System.Diagnostics;

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
            context!.ProjectCollectionView.ItemsSource = newvalue as ObservableRangeCollection<ChatListProjectViewModel>;
        }
        );

    public ObservableRangeCollection<ChatListProjectViewModel> ProjectCollection
    {
        get => (ObservableRangeCollection<ChatListProjectViewModel>)GetValue(ProjectCollectionProperty);
        set => SetValue(ProjectCollectionProperty, value);
    }

    public ProjectListView()
        => InitializeComponent();

    // project chatlist selection changed event handler
    public async void ProjectCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ChatListProjectViewModel chat = (ChatListProjectViewModel)e.CurrentSelection.SingleOrDefault();
        if (chat is null)
        {
            Debug.WriteLine("Channels Page: Chat is null!");
            return;
        }

        /*
         * NOTE:
         * 
         * Finish chat selection logic
         * bind projectlistview with a new messageview somehow!
         * 
         * */

        //if (context is null)
        //{
        //    Debug.WriteLine($"RoomsPageModel context is null!");
        //    return;
        //}

        //// set room id for loading messages!
        //UserStorage.RoomId = chat.Id;
        //Debug.WriteLine($"RoomId: {chat.Id}");

        //// set senderName in pagemodel
        //context.Sender = selectedChat.Title;
        //Debug.WriteLine($"sender name: {.Sender}");

        //// clear messageCollection before flush
        //context.MessageCollection.Clear();

        //// remove existing socket conn before reconnection!
        //if (_roomService._hub is not null)
        //    await _roomService.DisconnectWebSocket();
        //await _roomService.InitSignalR(context.MessageCollection, UserStorage.RoomId);
    }
}