using Shiemi.Dtos;
using Shiemi.Utilities;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Shiemi.Services;

public class DevService
{
    private readonly RestClient _restClient;
    private readonly HttpClient _httpClient;
    private readonly string devBaseUri;

    public DevService(RestClient restClient)
    {
        _restClient = restClient;
        _httpClient = _restClient.GetClient();
        devBaseUri = $"{_httpClient.BaseAddress}/Dev";
    }

    public async Task<bool> Create(DevDto dto, string advertPhotoPath)
    {
        var advertContent = new ByteArrayContent(
            await File.ReadAllBytesAsync(advertPhotoPath));
        using var form = new MultipartFormDataContent
        {
            { new StringContent(dto.UserId.ToString()),"id" },
            { new StringContent(dto.ShortDesc),"shortDesc" },
            { new StringContent(dto.Description),"description" },
            { new StringContent(dto.StartingPrice.ToString()),"startingPrice" },
            { advertContent,"advertPhoto",Path.GetFileName(advertPhotoPath) }
        };
        HttpResponseMessage response = await _httpClient.PostAsync(
            $"{devBaseUri}",
            form
        );
        if (response.IsSuccessStatusCode is false)
        {
            var statusDto = await response.Content.ReadFromJsonAsync<StatusMessageDto>();
            Debug.WriteLine(response.StatusCode);
            Debug.WriteLine(statusDto!.Message);
            return false;
        }

        return true;
    }

    public async Task<bool> SetDeveloper(int userId)
    {
        var response = await _httpClient.GetAsync(
            $"{devBaseUri}/{userId}/set-dev"
        );

        if (response.IsSuccessStatusCode is false)
            return false;

        return true;
    }
    public async Task<bool> ResetDeveloper(int userId)
    {
        var response = await _httpClient.GetAsync(
            $"{devBaseUri}/{userId}/reset-dev"
        );

        if (response.IsSuccessStatusCode is false)
            return false;

        return true;
    }

    public async Task<List<DevDto>?> GetAll()
        => await _httpClient.GetFromJsonAsync<List<DevDto>>(
            $"{devBaseUri}/all"
        );
	public async Task<DevDto?> GetByUserId(int userId)
		=> await _httpClient.GetFromJsonAsync<DevDto>(
            $"{devBaseUri}/{userId}/userId/dev"
		);
}
