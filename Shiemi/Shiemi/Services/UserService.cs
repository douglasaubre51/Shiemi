using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Utilities;
using System.Diagnostics;
using System.Net.Http.Json;

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

    public async Task<bool> Update(User user, string profilePath)
    {
        var profileContent = new ByteArrayContent(await File.ReadAllBytesAsync(profilePath));

        using var form = new MultipartFormDataContent();
        form.Add(new StringContent(user.Id.ToString()), "id");
        form.Add(new StringContent(user.FirstName), "firstName");
        form.Add(new StringContent(user.LastName), "lastName");
        form.Add(profileContent, "profilePhoto", Path.GetFileName(profilePath));

        var result = await _httpClient.PutAsync(
            $"{userBaseUri}",
            form
            );
        string? msg = await result.Content.ReadFromJsonAsync<string>();
        Debug.WriteLine(msg);
        if (result.IsSuccessStatusCode is false)
            return false;

        return true;
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
