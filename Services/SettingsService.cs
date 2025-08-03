using Shiemi.Services.Storage;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shiemi.Services
{
    public class SettingsService
    {
        // di
        readonly IConnectivity _connectivity;
        readonly JsonSerializerOptions _jsonCasing;

        // global vars
        string _userId;

        public SettingsService(IConnectivity connectivity, JsonSerializerOptions jsonCasing)
        {
            _connectivity = connectivity;
            _jsonCasing = jsonCasing;

            _userId = UserStorage.GetUserId();
        }

        // PUT: settings/dev-mode
        // update dev mode field in settings
        public async Task<bool> UpdateDeveloperModeSetting(bool value)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("offline!");
                return false;
            }

            int decimalValue = (value is true) ? 1 : 0;
            var payload = new
            {
                DeveloperMode = decimalValue
            };

            string url = "http://localhost:3000/settings/dev-mode/" + _userId;
            using HttpClient client = new();

            HttpResponseMessage response = await client.PutAsJsonAsync(url, payload, _jsonCasing);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
