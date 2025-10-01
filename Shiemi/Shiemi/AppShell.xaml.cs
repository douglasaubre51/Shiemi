using Shiemi.Pages;

namespace Shiemi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Profile), typeof(Profile));
        }
    }
}
