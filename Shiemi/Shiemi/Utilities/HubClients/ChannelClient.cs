using Microsoft.AspNetCore.SignalR.Client;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Utilities.HubClients;

public class ChannelClient(EnvironmentStorage envStorage)
{
    private readonly EnvironmentStorage _envStorage = envStorage;

    private HubConnection? _conn;

    public async Task StartClient(int channelId)
    {
        _conn = new HubConnectionBuilder()
           .WithAutomaticReconnect()
           .WithUrl(_envStorage.GetSHIEMIWebsocketUri() + "/channel")
           .Build();

        _conn.Closed += async (err) =>
        {
            Debug.WriteLine($"socket conn closed: {err!.Message}");
        };


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
}
