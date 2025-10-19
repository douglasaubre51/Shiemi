using Shiemi.Pages;

namespace Shiemi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Startup commands here
            Preferences.Default.Clear();


            // Register routes here
            Routing.RegisterRoute(nameof(CreateProject), typeof(CreateProject));
            Routing.RegisterRoute(nameof(ProjectDetails), typeof(ProjectDetails));
            Routing.RegisterRoute(nameof(PrivateRoom), typeof(PrivateRoom));
        }
    }
}
