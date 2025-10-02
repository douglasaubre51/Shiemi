using Shiemi.Storage;

namespace Shiemi.Services;

public class RestClient
{
    private readonly HttpClient _httpClient;
    private readonly EnvironmentStorage _envStorage;

    public RestClient(EnvironmentStorage envStorage)
    {
        _envStorage = envStorage;
        _httpClient = new HttpClient();

        // Configure HttpClient!
        _httpClient.BaseAddress = new Uri(_envStorage.GetSHIEMIBaseUri());
    }

    public HttpClient GetClient()
        => _httpClient;
}
