using System.Net.Http.Json;
using Shiemi.Dtos;
using Shiemi.Utilities;

namespace Shiemi.Services;

public class UserService
{
    private readonly RestClient _restClient;
    private readonly HttpClient _httpClient;
    private readonly string userBaseUri;

    public UserService(RestClient restClient)
    {
        _restClient = restClient;
        _httpClient = _restClient.GetClient();
        userBaseUri = $"{_httpClient.BaseAddress}/User";
    }

    public async Task<ProfilePageUserDto?> Get(string userId)
        => await _httpClient.GetFromJsonAsync<ProfilePageUserDto>(
        $"{userBaseUri}/id/{userId}"
        );
    public async Task<UserDto?> GetUserId(string userId)
    {
        var response = await _httpClient.GetAsync(
            $"{userBaseUri}/{userId}/id"
            );
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
    public async Task<UserDto?> GetUserById(int id)
    {
        var response = await _httpClient.GetAsync(
            $"{userBaseUri}/{id}"
            );
        if (!response.IsSuccessStatusCode)
            return null!;

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
}
