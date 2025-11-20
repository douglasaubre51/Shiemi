using Shiemi.Dtos;
using Shiemi.Utilities;
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
    {
        var response = await _httpClient.GetAsync(
            $"{userBaseUri}/{userId}/id"
            );
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserIdDto>();
    }

    public async Task<AccountDto?> GetUserById(int id)
    {
        var response = await _httpClient.GetAsync(
            $"{userBaseUri}/{id}"
            );
        if (!response.IsSuccessStatusCode)
            return null!;

        return await response.Content.ReadFromJsonAsync<AccountDto>();
    }
}
