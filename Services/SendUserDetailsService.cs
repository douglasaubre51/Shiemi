using Shiemi.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Shiemi.Services
{
    public class SendUserDetailsService
    {
        string url = "http://localhost:3000/sign-up";
        string message;

        // di
        private readonly JsonSerializerOptions _jsonCasing;

        public SendUserDetailsService(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonCasing = jsonSerializerOptions;
        }

        public async Task Send(User user)
        {
            Debug.WriteLine("sending post request!");

            // create payload
            string payloadString = JsonSerializer.Serialize(user, _jsonCasing);
            HttpContent payload = new StringContent(payloadString, Encoding.UTF8, "application/json");

            // send
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(url, payload);

            // server says no!
            if (!response.IsSuccessStatusCode)
            {
                message = await response.Content.ReadAsStringAsync();
                await Shell.Current.DisplayAlertAsync(
                    "signing in error",
                    $"{message}",
                    "ok"
                    );
                return;
            }

            // server says yes
            Debug.WriteLine("Sucess signed iN!");
        }
    }
}
