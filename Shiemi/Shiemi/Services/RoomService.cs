using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using Shiemi.Dtos.MessageDtos;
using Shiemi.Storage;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Shiemi.Services;

public class RoomService
{
    private readonly HttpClient _httpClient;
    private readonly EnvironmentStorage _envStorage;

    private string roomBaseURI;
    public HubConnection _hub;

    public RoomService(
        RestClient restClient,
        EnvironmentStorage envStorage
        )
    {
        _httpClient = restClient.GetClient();
        roomBaseURI = $"{_httpClient.BaseAddress}/Room";
        _envStorage = envStorage;
    }

    public async Task<int> GetPrivateRoom(int userId, int projectId)
    {
        var response = await _httpClient.GetAsync(
              $"{roomBaseURI}/Private/{userId}/{projectId}"
            );
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"FAIL: GetPrivateRoom status: {response.StatusCode}");
            return await Task.FromResult<int>(0);
        }

        return await response.Content.ReadFromJsonAsync<int>();
    }

    // SignalR pipeline
    public async Task InitSignalR(
        ObservableRangeCollection<MessageDto> dtoCollection,
        int roomId
        )
    {
        _hub = new HubConnectionBuilder()
            .WithUrl(_envStorage.GetSHIEMIWebsocketUri() + "/room")
            .WithAutomaticReconnect()
            .Build();

        _hub.Closed += async (error)
            => Debug.WriteLine("websocket disconnected!");

        // load all previous chats
        _hub.On<List<MessageDto>>(
            "LoadChat",
            async (dtos) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    dtoCollection.AddRange(dtos);
                });
            });

        // update msg from hub
        _hub.On<MessageDto>(
            "UpdateChat",
            async (dto) =>
                MainThread.BeginInvokeOnMainThread(() => dtoCollection.Add(dto))
            );

        // start _hub
        await _hub.StartAsync();

        Debug.WriteLine($"created websocket");

        // set userId and room at hub
        await _hub.InvokeAsync(
            "SetUserIdAndRoom",
            UserStorage.UserId,
            roomId
            );
    }

    public async Task SendChat(MessageDto dto)
        => await _hub.InvokeAsync("SendChat", dto);

    public async Task ReconnectWebSocket()
        => await _hub.StartAsync();
    public async Task DisconnectWebSocket()
        => await _hub.StopAsync();
}
