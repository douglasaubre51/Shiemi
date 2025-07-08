using Shiemi.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Shiemi.Services
{
    public class SendUserDetailsService
    {
        string url = "http://localhost:5000/sign-up";
        string message;

        public async Task Send(User user)
        {
            Debug.WriteLine("sending post request!");

            // create payload
            string payloadString = JsonSerializer.Serialize(user);
            HttpContent payload = new StringContent(payloadString);

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
