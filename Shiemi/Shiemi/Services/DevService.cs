using System.Net.Http.Json;
using System.Resources;
using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Utilities;

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

    public async Task<bool> Create(DevModel model)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<DevModel>(
            $"{devBaseUri}",
            model
        );
        if (response.IsSuccessStatusCode is false)
            return false;

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
}
