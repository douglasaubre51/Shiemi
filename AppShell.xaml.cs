using Shiemi.Views;

namespace Shiemi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // register routes
            Routing.RegisterRoute("CreateProjectView", typeof(CreateProjectView));
            Routing.RegisterRoute("SignInView", typeof(SignInView));
            Routing.RegisterRoute("ProfileView", typeof(UserProfileView));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("SignInView");
        }
    }
}
