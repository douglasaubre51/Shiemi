namespace Shiemi.Storage;

public class EnvironmentStorage
{
    private string WAGURI_LOGIN_URI = "https://waguri-hofi.onrender.com/api/NativeAuth/Login/002";
    private string WAGURI_LOGIN_HUB_URI = "https://waguri-hofi.onrender.com/native-auth";

    //private string SHIEMI_BASE_URI = "https://shiemiapi.onrender.com/api";
    //private string SHIEMI_HUB_URI = "https://shiemiapi.onrender.com/hubs";
    private string SHIEMI_BASE_URI = "https://localhost:7268/api";
    private string SHIEMI_HUB_URI = "https://localhost:7268/hubs";

    // WAGURI SCS env
    public string GetWAGURILoginUri()
        => WAGURI_LOGIN_URI;
    public string GetWAGURIWebsocketUri()
        => WAGURI_LOGIN_HUB_URI;

    // SHIEMI api env
    public string GetSHIEMIBaseUri()
        => SHIEMI_BASE_URI;
    // SHIEMI SignalR hub env
    public string GetSHIEMIWebsocketUri()
        => SHIEMI_HUB_URI;
}
