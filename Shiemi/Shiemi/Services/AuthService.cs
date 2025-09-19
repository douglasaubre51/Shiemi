using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace Shiemi.Services
{
    public class AuthService(EnvironmentService envService)
    {
        private readonly EnvironmentService _envService = envService;

        public async Task ConnectToWAGURI()
        {
            var conn = new HubConnectionBuilder()
                .WithUrl(_envService.GetWAGURIWebsocketUri())
                .Build();

            conn.Closed += async (error) =>
            {
                Debug.WriteLine("websocket disconnected!");
            };

            Debug.WriteLine("connecting to WaGURI ... ");
            await conn.StartAsync();
        }
    }
}
