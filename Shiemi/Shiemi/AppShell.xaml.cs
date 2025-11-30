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
            Routing.RegisterRoute(nameof(Details), typeof(Details));
            // Market
            Routing.RegisterRoute(nameof(ProjectDetails), typeof(ProjectDetails));
            Routing.RegisterRoute(nameof(PrivateRoom), typeof(PrivateRoom));
        }
    }
}
