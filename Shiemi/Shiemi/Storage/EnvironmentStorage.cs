namespace Shiemi.Storage;

public class EnvironmentStorage
{
    public EnvironmentStorage()
    {
        Preferences.Default.Set(
            "WAGURI_LOGIN_URI",
            "http://localhost:5020/api/NativeAuth/Login/002"
            );
        Preferences.Default.Set(
            "WAGURI_WEBSOCKET_URI",
            "http://localhost:5020/native-auth"
            );

        Preferences.Default.Set(
            "SHIEMI_BASE_URI",
            "https://localhost:7268/api"
            );
    }

    // WAGURI SCS env
    public string GetWAGURILoginUri()
        => Preferences.Default.Get("WAGURI_LOGIN_URI", "");
    public string GetWAGURIWebsocketUri()
        => Preferences.Default.Get("WAGURI_WEBSOCKET_URI", "");

    // SHIEMI api env
    public string GetSHIEMIBaseUri()
        => Preferences.Default.Get("SHIEMI_BASE_URI", "");
}
