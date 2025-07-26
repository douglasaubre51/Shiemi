using Shiemi.Views;

namespace Shiemi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // register routes
            Routing.RegisterRoute(nameof(SignInView), typeof(SignInView));
            Routing.RegisterRoute(nameof(SignUpView), typeof(SignUpView));
            Routing.RegisterRoute(nameof(UserProfileView), typeof(UserProfileView));
            Routing.RegisterRoute(nameof(CreateProjectView), typeof(CreateProjectView));
            Routing.RegisterRoute(nameof(ProjectInfoView), typeof(ProjectInfoView));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SignInView));
        }
    }
}
