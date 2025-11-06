using Shiemi.Dtos.RoomDtos;
using Shiemi.Storage;
using Shiemi.Wrappers;
using System.Net.Http.Json;

namespace Shiemi.Services;

public class ChatService
{
    private readonly HttpClient _httpClient;
    private readonly EnvironmentStorage _envStorage;

    private string chatBaseURI;

    public ChatService(
        RestClient restClient,
        EnvironmentStorage envStorage
        )
    {
        _httpClient = restClient.GetClient();
        chatBaseURI = $"{_httpClient.BaseAddress}/Room";
        _envStorage = envStorage;
    }

    public async Task<List<RoomDto>?> GetAllRooms()
    {
        var response = await _httpClient.GetAsync(
            $"{chatBaseURI}/Private/{UserStorage.UserId}/all"
            );
        if (!response.IsSuccessStatusCode)
            return null;

        var wrap = await response.Content.ReadFromJsonAsync<RoomsWrap>();
        return wrap!.Rooms;
    }
}
