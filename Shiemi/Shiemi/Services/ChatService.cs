using Shiemi.Dtos.RoomDtos;
using Shiemi.Storage;
using Shiemi.Wrappers;
using System.Net.Http.Json;

namespace Shiemi.Services;

public class ChatService
{
    private readonly HttpClient _httpClient;
    private readonly EnvironmentStorage _envStorage;
    private readonly UserStorage _userStorage;

    private string chatBaseURI;

    public ChatService(
        RestClient restClient,
        EnvironmentStorage envStorage,
        UserStorage userStorage
        )
    {
        _httpClient = restClient.GetClient();
        chatBaseURI = $"{_httpClient.BaseAddress}/Room";
        _envStorage = envStorage;
        _userStorage = userStorage;
    }

    public async Task<List<RoomDto>?> GetAllRooms()
    {
        var response = await _httpClient.GetAsync(
            $"{chatBaseURI}/Private/{_userStorage.UserId}/all"
            );
        if (!response.IsSuccessStatusCode)
            return null;

        var wrap = await response.Content.ReadFromJsonAsync<RoomsWrap>();
        return wrap!.Rooms;
    }
}
