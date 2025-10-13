using Shiemi.Pages;

namespace Shiemi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes
            Routing.RegisterRoute(nameof(CreateProject), typeof(CreateProject));

            // Startup commands
            Preferences.Default.Clear();
        }
    }
}
