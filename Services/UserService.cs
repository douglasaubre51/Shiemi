using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Shiemi.Models;

namespace Shiemi.Services;

public class UserService
{
    string url = "http://localhost:3000/sign-in";

    // di
    private readonly JsonSerializerOptions _jsonCasing;

    public UserService(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonCasing = jsonSerializerOptions;
    }

    public async Task<bool> RequestSignIn(SignIn signIn)
    {
        string payloadString = JsonSerializer.Serialize(signIn, _jsonCasing);
        HttpContent content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        // send post request!
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.PostAsync(url, content);

        // failure
        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"{message}");

            await Shell.Current.DisplayAlertAsync(
                "sign in error!",
                $"{message}",
                "try again"
            );

            return false;
        }

        // success
        return true;
    }
}
