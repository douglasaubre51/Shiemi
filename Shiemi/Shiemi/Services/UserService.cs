using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Utilities;
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

    public async Task<bool> CheckIfReviewIsAllowed(int userId, int projectId)
    {
        var response = await _httpClient.GetAsync(
            $"{userBaseUri}/{userId}/past-projects");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("CheckIfReviewIsAllowed get error: " + response.IsSuccessStatusCode);
            return false;
        }

        List<int>? pastProjects = await response.Content.ReadFromJsonAsync<List<int>>();
        return pastProjects!.Contains(projectId);
    }

    // Update User Profile without Profile Photo!
    public async Task<bool> UpdateUserDetails(
        User user,
        OptionalUserDetails optionalUserDetails)
    {
        var result = await _httpClient.PutAsJsonAsync(
            $"{userBaseUri}/user-details",
            new
            {
                UserDto = user,
                OptionalUserDetailsDto = optionalUserDetails
            }
            );

        if (result.IsSuccessStatusCode is false)
        {
            var dto = await result.Content.ReadFromJsonAsync<StatusMessageDto>();
            Debug.WriteLine(dto!.Message);

            return false;
        }

        return true;
    }
    public async Task<bool> UpdateUserProfilePhoto(int id, string profilePath)
    {
        var profileContent = new ByteArrayContent(await File.ReadAllBytesAsync(profilePath));
        using var form = new MultipartFormDataContent
        {
            { new StringContent(id.ToString()), "id" },
            { profileContent, "profilePhoto", Path.GetFileName(profilePath) }
        };

        var result = await _httpClient.PutAsync(
            $"{userBaseUri}/user-profile-photo",
            form
            );

        var dto = await result.Content.ReadFromJsonAsync<StatusMessageDto>();
        Debug.WriteLine(dto!.Message);
        if (result.IsSuccessStatusCode is false)
            return false;

        return true;
    }

    // Get Optional User details via id!
    public async Task<OptionalUserDetails?> GetOptionalDetails(int userId)
        => await _httpClient.GetFromJsonAsync<OptionalUserDetails>(
            $"{userBaseUri}/{userId}/optional-details");

    // get dbuser via string id!
    public async Task<ProfilePageUserDto?> Get(string userId)
        => await _httpClient.GetFromJsonAsync<ProfilePageUserDto>(
        $"{userBaseUri}/id/{userId}"
        );

    // get integer id via string id!
    public async Task<UserDto?> GetUserId(string userId)
    {
        var response = await _httpClient.GetAsync(
            $"{userBaseUri}/{userId}/id"
            );
        if (!response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<StatusMessageDto>();
            Debug.WriteLine(dto!.Message);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    // get dbuser data by integer id!
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
