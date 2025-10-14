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

            // Startup commands here
        }
    }
}
