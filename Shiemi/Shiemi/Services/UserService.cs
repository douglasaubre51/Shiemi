using Shiemi.Dtos.ProfileDtos;
using Shiemi.Dtos.UserDtos;
using System.Net.Http.Json;

namespace Shiemi.Services;
public class UserService
{
    private readonly RestClient _restClient;
    private readonly HttpClient _httpClient;
    private string userBaseUri;

    public UserService(RestClient restClient)
    {
        _restClient = restClient;
        _httpClient = _restClient.GetClient();
        userBaseUri = $"{_httpClient.BaseAddress}/User";
    }

    public async Task<UserDto?> Get(string userId)
        => await _httpClient.GetFromJsonAsync<UserDto>(
        $"{userBaseUri}/id/{userId}"
        );

    public async Task<UserIdDto?> GetUserId(string userId)
        => await _httpClient.GetFromJsonAsync<UserIdDto>(
            $"{userBaseUri}/{userId}/id"
            );
}
