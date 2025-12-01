using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.HubModels;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Shiemi.Utilities.HubClients;

public class RoomClient
{
    private readonly HttpClient _httpClient;
    private readonly EnvironmentStorage _envStorage;

    private readonly string roomBaseURI;

    public HubConnection? _hub;

    public RoomClient(
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

    // SignalR pipeline initializer
    public async Task InitSignalR(
        ObservableRangeCollection<MessageViewModel> messageCollection,
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
        _hub.On<List<RoomMessageHubModel>>(
            "LoadChat",
            async (dtos) =>
            {
                // set IsOwner for roomMessageHubModels
                var ownerMessages = dtos.Where(c => c.UserId == UserStorage.UserId)
                    .ToList();
                foreach (var m in ownerMessages)
                {
                    Debug.WriteLine("id: " + m.Id);
                    Debug.WriteLine("userid: " + UserStorage.UserId);
                    m.IsOwner = true;
                }

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, MessageViewModel>();
                if (mapper is null)
                {
                    Debug.WriteLine("RoomClient: LoadChat: error: GetMapper returned null!");
                    return;
                }

                // flush messageCollection on MessageView
                var messageViewModels = mapper.Map<List<MessageViewModel>>(dtos);
                await MainThread.InvokeOnMainThreadAsync(() =>
                    messageCollection.ReplaceRange(messageViewModels)
                );
            });

        // update msg from hub
        _hub.On<RoomMessageHubModel>(
            "UpdateChat",
            async (dto) =>
            {
                // set IsOwner for Message
                if (dto.UserId == UserStorage.UserId)
                    dto.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, MessageViewModel>();
                if (mapper is null)
                {
                    Debug.WriteLine("RoomClient: LoadChat: error: GetMapper returned null!");
                    return;
                }

                var messageViewModel = mapper.Map<MessageViewModel>(dto);
                // flush to MessageCollection on MessageView
                MainThread.BeginInvokeOnMainThread(() => messageCollection.Add(messageViewModel));
            });

        await _hub.StartAsync();
        Debug.WriteLine($"RoomClient: websocket initialized!");

        // set userId and room at hub
        await _hub.InvokeAsync(
            "SetUserIdAndRoom",
            UserStorage.UserId,
            roomId
            );
    }

    // SignalR actions
    public async Task DisconnectWebSocket()
        => await _hub!.StopAsync();
    public async Task ReconnectWebSocket()
        => await _hub!.StartAsync();
    public async Task SendChat(SendMessageDto dto)
        => await _hub!.InvokeAsync("SendChat", dto);
}
