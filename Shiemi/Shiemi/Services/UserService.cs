using Shiemi.Dtos.ProfileDtos;
using System.Net.Http.Json;

namespace Shiemi.Services;
public class UserService
{
    private readonly RestClient _restClient;
    private readonly HttpClient _httpClient;

    public UserService(RestClient restClient)
    {
        _restClient = restClient;
        _httpClient = _restClient.GetClient();
    }

    public async Task<User?> Get(string userId)
        => await _httpClient.GetFromJsonAsync<User>(
        $"/get-user/id/{userId}"
        );
}
