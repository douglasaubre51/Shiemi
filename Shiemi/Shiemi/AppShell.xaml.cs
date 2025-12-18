using Shiemi.Pages.Dev;
using Shiemi.Pages.Market;
using Shiemi.Pages.Project;

namespace Shiemi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes
            // Project
            Routing.RegisterRoute(nameof(CreateProject), typeof(CreateProject));
            Routing.RegisterRoute(nameof(Pages.Project.Details), typeof(Pages.Project.Details));
            // Market
            Routing.RegisterRoute(nameof(ProjectDetails), typeof(ProjectDetails));
            Routing.RegisterRoute(nameof(PrivateRoom), typeof(PrivateRoom));
            // Dev
            Routing.RegisterRoute(nameof(Edit), typeof(Edit));
        }
    }
}
