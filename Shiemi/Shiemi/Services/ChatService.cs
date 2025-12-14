using Shiemi.Dtos;
using Shiemi.Storage;
using Shiemi.Utilities;
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

    public async Task<List<MessageDto>?> GetAllByRoom(int id)
    {
        var response = await _httpClient.GetAsync(
            $"{chatBaseURI}/Private/{id}/messages"
            );
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<MessageDto>>();
    }
}
