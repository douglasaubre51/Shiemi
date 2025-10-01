using Microsoft.AspNetCore.SignalR.Client;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Services;

public class AuthService(
    EnvironmentStorage envService,
    UserStorage userStorage
    )
{
    private readonly EnvironmentStorage _envService = envService;
    private readonly UserStorage _userStorage = userStorage;

    public async Task ConnectToWAGURI(string clientGuid)
    {
        var conn = new HubConnectionBuilder()
            .WithUrl(_envService.GetWAGURIWebsocketUri())
            .Build();

        conn.Closed += async (error) =>
        {
            Debug.WriteLine("websocket disconnected!");
        };

        conn.On<string>(
            "GetGreeting",
            (message) => Debug.WriteLine(message)
            );

        conn.On<string>(
            "LoginSuccess",
            (userId) =>
            {
                Debug.WriteLine($"userId: {userId}");
                _userStorage.UserId = userId;
                DataStorage.Store("UserId", userId);
            });

        conn.On(
            "GetClientGuid",
            () => clientGuid
            );

        await conn.StartAsync();

        await conn.InvokeAsync("CallGetClientGuid");
    }
}
