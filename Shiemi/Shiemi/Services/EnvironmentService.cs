namespace Shiemi.Services
{
    public class EnvironmentService
    {
        public EnvironmentService()
        {
            Preferences.Default.Set<string>(
                "WAGURI_LOGIN_URI",
                "http://localhost:5020/api/NativeAuth/Login/002"
                );
            Preferences.Default.Set<string>(
                "WAGURI_WEBSOCKET_URI",
                "http://localhost:5020/native-auth"
                );
        }

        public string GetWAGURILoginUri()
            => Preferences.Default.Get<string>("WAGURI_LOGIN_URI", "");
        public string GetWAGURIWebsocketUri()
            => Preferences.Default.Get<string>("WAGURI_WEBSOCKET_URI", "");
    }
}
