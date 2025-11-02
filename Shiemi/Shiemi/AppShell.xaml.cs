using Shiemi.Pages;

namespace Shiemi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes here
            Routing.RegisterRoute(nameof(CreateProject), typeof(CreateProject));
            Routing.RegisterRoute(nameof(ProjectDetails), typeof(ProjectDetails));
            Routing.RegisterRoute(nameof(PrivateRoom), typeof(PrivateRoom));
        }
    }
}
