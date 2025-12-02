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


    // fetch room id

    public async Task<int> GetPrivateRoom(int userId, int projectId)
    {
        var response = await _httpClient.GetAsync(
              $"{roomBaseURI}/Private/{userId}/{projectId}"
            );
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"GetPrivateRoom status: {response.StatusCode}");
            return await Task.FromResult<int>(0);  // for safety !
        }

        return await response.Content.ReadFromJsonAsync<int>();
    }


    public async Task InitSignalR(
        ObservableRangeCollection<MessageViewModel> messageCollection,
        int roomId)
    {
        _hub = new HubConnectionBuilder()
            .WithUrl(_envStorage.GetSHIEMIWebsocketUri() + "/room")
            .WithAutomaticReconnect()
            .Build();

        _hub.Closed += async (error)
            => Debug.WriteLine("websocket disconnected!");

        _hub.On<List<RoomMessageHubModel>>(
            "LoadChat",
            async (dtos) =>
            {
                var ownerMessages = dtos.Where(c => c.UserId == UserStorage.UserId)
                    .ToList();
                foreach (var m in ownerMessages)
                    m.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, MessageViewModel>();
                if (mapper is null)
                    return;

                var messageViewModels = mapper.Map<List<MessageViewModel>>(dtos);
                await MainThread.InvokeOnMainThreadAsync(() =>
                    messageCollection.ReplaceRange(messageViewModels)
                );
            });


        // message notification from hub !

        _hub.On<RoomMessageHubModel>(
            "UpdateChat",
            async (dto) =>
            {
                if (dto.UserId == UserStorage.UserId)
                    dto.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, MessageViewModel>();
                if (mapper is null)
                    return;

                var messageViewModel = mapper.Map<MessageViewModel>(dto);
                MainThread.BeginInvokeOnMainThread(() => messageCollection.Add(messageViewModel));
            });

        await _hub.StartAsync();
        Debug.WriteLine($"RoomClient: websocket initialized!");

        await _hub.InvokeAsync(
            "SetUserIdAndRoom",
            UserStorage.UserId,
            roomId);
    }


    // SignalR actions

    public async Task SendChat(SendMessageDto dto)
        => await _hub!.InvokeAsync("SendChat", dto);
    public async Task DisconnectWebSocket()
        => await _hub!.StopAsync();
    public async Task ReconnectWebSocket()
        => await _hub!.StartAsync();
}
