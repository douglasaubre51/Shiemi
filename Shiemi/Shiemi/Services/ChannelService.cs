using Shiemi.Models;
using Shiemi.Utilities;
using System.Net.Http.Json;

namespace Shiemi.Services;

public class ChannelService
{
    private readonly HttpClient _client;
    private readonly string _channelBaseURI;

    public ChannelService(RestClient restClient)
    {
        _client = restClient.GetClient();
        _channelBaseURI = $"{_client.BaseAddress}/Channel";
    }

    public async Task<Channel?> GetById(int id)
    {
        var response = await _client.GetAsync($"{_channelBaseURI}/{id}");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine(response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<Channel>();
    }
}
