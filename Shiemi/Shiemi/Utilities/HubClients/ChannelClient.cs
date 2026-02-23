using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.HubModels;
using Shiemi.Models;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;

namespace Shiemi.Utilities.HubClients;

public class ChannelClient(
    EnvironmentStorage envStorage
)
{
    private readonly EnvironmentStorage _envStorage = envStorage;

    public HubConnection? _conn;

    public async Task StartClient(
        int channelId,
        ObservableRangeCollection<Message> MessageCollection
        )
    {
        _conn = new HubConnectionBuilder()
           .WithAutomaticReconnect()
           .WithUrl(_envStorage.GetSHIEMIWebsocketUri() + "/channel")
           .Build();

        _conn.Closed += async (err) =>
            Debug.WriteLine($"socket conn closed !");

        _conn.On<RoomMessageHubModel>(
            "UpdateChat",
            async (dto) =>
            {
                if (dto.UserId == UserStorage.UserId)
                    dto.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, Message>();
                Message newMessage = mapper!.Map<Message>(dto);

                MainThread.BeginInvokeOnMainThread(() => MessageCollection.Add(newMessage));
            });

        // Load database chats !
        _conn.On<List<RoomMessageHubModel>>(
            "LoadChat",
            async (dtos) =>
            {
                if (dtos is null)
                    return;

                // Owner chats go left !
                var ownerMessages = dtos.Where(c => c.UserId == UserStorage.UserId)
                    .ToList();
                foreach (var m in ownerMessages)
                    m.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, Message>();
                List<Message> oldMessages = mapper!.Map<List<Message>>(dtos);

                await MainThread.InvokeOnMainThreadAsync(() =>
                    MessageCollection.AddRange(oldMessages)
                );
            });

        await _conn.StartAsync();
        await _conn.InvokeAsync(
            "Init",
            UserStorage.UserId,
            channelId
        );
    }

    // Channel client actions:
    public async Task StopClient()
        => await _conn!.StopAsync();
    public async Task RestartClient()
        => await _conn!.StartAsync();
    public async Task SendChat(SendMessageDto dto)
        => await _conn!.InvokeAsync("UploadChat", dto);
}
