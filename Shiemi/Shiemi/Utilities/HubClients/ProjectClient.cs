using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Utilities.HubClients;

public class ProjectClient(
    EnvironmentStorage envStorage
    )
{
    private readonly EnvironmentStorage _envStorage = envStorage;

    private HubConnection _hub = null!;


    // SignalR pipeline
    public async Task InitSignalR(
        ObservableRangeCollection<ProjectDto> dtoCollection,
        int userId
        )
    {
        _hub = new HubConnectionBuilder()
            .WithUrl(_envStorage.GetSHIEMIWebsocketUri() + "/project")
            .WithAutomaticReconnect()
            .Build();

        _hub.Closed += async (error)
            => Debug.WriteLine($"websocket disconnected: {error!.Message}");

        // load data into collection
        _hub.On<List<ProjectDto>>(
            "LoadCollection",
            async (dtos) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                    dtoCollection.AddRange(dtos)
                );
            });

        // update collection data
        _hub.On<ProjectDto>(
            "UpdateCollection",
            async (dto) =>
                MainThread.BeginInvokeOnMainThread(() => dtoCollection.Add(dto))
            );

        // start hub
        await _hub.StartAsync();
        Debug.WriteLine($"created Project socket!");

        // set userId
        await _hub.InvokeAsync("Init", UserStorage.UserId);
    }

    public async Task ReconnectWebSocket()
        => await _hub.StartAsync();
    public async Task DisconnectWebSocket()
        => await _hub.StopAsync();
}
