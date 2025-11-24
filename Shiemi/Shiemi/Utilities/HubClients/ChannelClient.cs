using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using Shiemi.HubModels;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;
using System.Diagnostics;

namespace Shiemi.Utilities.HubClients;

public class ChannelClient(
    EnvironmentStorage envStorage
    )
{
    private readonly EnvironmentStorage _envStorage = envStorage;

    private HubConnection? _conn;

    public async Task StartClient(
        ObservableRangeCollection<MessageViewModel> messageCollection,
        int channelId
        )
    {
        _conn = new HubConnectionBuilder()
           .WithAutomaticReconnect()
           .WithUrl(_envStorage.GetSHIEMIWebsocketUri() + "/channel")
           .Build();

        _conn.Closed += async (err) =>
        {
            Debug.WriteLine($"socket conn closed: {err!.Message}");
        };

        // load previous chats
        _conn.On<List<RoomMessageHubModel>>(
            "LoadChat",
            async (dtos) =>
            {
                // set IsOwner for roomMessageHubModels
                var ownerMessages = dtos.Where(c => c.Id == UserStorage.UserId)
                    .ToList();
                foreach (var m in ownerMessages)
                    m.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, MessageViewModel>();
                if (mapper is null)
                {
                    Debug.WriteLine("RoomClient: LoadChat: error: GetMapper returned null!");
                    return;
                }

                // flush messageCollection on MessageView
                var messageViewModels = mapper.Map<List<MessageViewModel>>(dtos);
                await MainThread.InvokeOnMainThreadAsync(() =>
                    messageCollection.AddRange(messageViewModels)
                );
            });

        await _conn.StartAsync();

        // register user on group
        await _conn.InvokeAsync(
            "SetUserIdAndChannel",
            UserStorage.UserId,
            channelId
            );
    }

    public async Task StopClient()
        => await _conn!.StopAsync();
    public async Task RestartClient()
        => await _conn!.StopAsync();
}
