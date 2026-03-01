using Shiemi.Pages.Market;
using Shiemi.Pages.Project;
using Shiemi.ViewModels;

namespace Shiemi;

public partial class AppShell : Shell
{
    public AppShell(FlyoutFooterModel flyoutFooterModel)
    {
        InitializeComponent();

        // Init flyout footer binding context!
        BindingContext = flyoutFooterModel;


        // Register routes
        // Project
        Routing.RegisterRoute(nameof(CreateProject), typeof(CreateProject));
        Routing.RegisterRoute(nameof(Details), typeof(Details));
        Routing.RegisterRoute("AddDevs", typeof(AddDevs));
        Routing.RegisterRoute("EditProject", typeof(EditProject));

        // Market
        Routing.RegisterRoute(nameof(ProjectDetails), typeof(ProjectDetails));
        Routing.RegisterRoute(nameof(PrivateRoom), typeof(PrivateRoom));

        // Dev
        Routing.RegisterRoute("EditDev", typeof(Pages.Dev.Edit));
        Routing.RegisterRoute("DetailsDev", typeof(Pages.Dev.Details));
        Routing.RegisterRoute("ChatDev", typeof(Pages.Dev.Chat));

        // User
        Routing.RegisterRoute("EditProfile", typeof(Pages.User.Edit));
        Routing.RegisterRoute("ViewMoreProjects", typeof(Pages.User.ViewMoreProjects));

        // Chats
        Routing.RegisterRoute("ChatRooms", typeof(Pages.Chats.Rooms));
        Routing.RegisterRoute("Channels", typeof(Pages.Chats.Channels));

        // Android Specific Pages
        // Private Room Chats
        Routing.RegisterRoute("ManagePrivateChats", typeof(Pages.Chats.PrivateRoomChats));
    }
}