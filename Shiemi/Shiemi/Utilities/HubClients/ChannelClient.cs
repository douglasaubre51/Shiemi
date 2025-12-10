using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.HubModels;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.Utilities.HubClients;

public class ChannelClient(
    EnvironmentStorage envStorage
)
{
    private readonly EnvironmentStorage _envStorage = envStorage;

    public HubConnection? _conn;

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
            Debug.WriteLine($"socket conn closed !");
        };

        _conn.On<RoomMessageHubModel>(
            "UpdateChat",
            async (dto) =>
            {
                Debug.WriteLine("updating message collection ...");
                if (dto.UserId == UserStorage.UserId)
                    dto.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, MessageViewModel>();
                if (mapper is null)
                    return;

                var messageViewModel = mapper.Map<MessageViewModel>(dto);
                MainThread.BeginInvokeOnMainThread(() => messageCollection.Add(messageViewModel));
            });

        // load previous chats
        _conn.On<List<RoomMessageHubModel>>(
            "LoadChat",
            async (dtos) =>
            {
                if (dtos is null)
                    return;

                var ownerMessages = dtos.Where(c => c.UserId == UserStorage.UserId)
                    .ToList();
                foreach (var m in ownerMessages)
                    m.IsOwner = true;

                Mapper? mapper = MapperProvider.GetMapper<RoomMessageHubModel, MessageViewModel>();
                if (mapper is null)
                {
                    Debug.WriteLine("ChannelClient: LoadChat: error: GetMapper returned null!");
                    return;
                }

                var messageViewModels = mapper.Map<List<MessageViewModel>>(dtos);
                await MainThread.InvokeOnMainThreadAsync(() =>
                    messageCollection.AddRange(messageViewModels)
                );
            });

        await _conn.StartAsync();

        await _conn.InvokeAsync(
            "Init",
            UserStorage.UserId,
            channelId
        );
    }

    public async Task StopClient()
        => await _conn!.StopAsync();
    public async Task RestartClient()
        => await _conn!.StartAsync();
    public async Task SendChat(SendMessageDto dto)
        => await _conn!.InvokeAsync("UploadChat", dto);
}
