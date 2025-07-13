using Shiemi.Dto;
using System.Text;
using System.Text.Json;

namespace Shiemi.Services;

public class UserService
{
    // di
    private readonly JsonSerializerOptions _jsonCasing;

    public UserService(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonCasing = jsonSerializerOptions;
    }

    // POST: sign-in
    public async Task<bool> RequestSignIn(SignInDto signIn)
    {
        string url = "http://localhost:3000/sign-in";

        string payloadString = JsonSerializer.Serialize(signIn, _jsonCasing);
        HttpContent content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        // send post request!
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.PostAsync(url, content);

        // failure
        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            await Shell.Current.DisplayAlertAsync(
                "sign in error!",
                $"{message}",
                "try again"
            );
            return false;
        }

        // success
        // fetch user id
        string responseJsonString = await response.Content.ReadAsStringAsync();
        UserIdDto? dto = JsonSerializer.Deserialize<UserIdDto>(responseJsonString, _jsonCasing);
        string userId = dto.UserId;

        return true;
    }

    // POST: sign-up
    public async Task<bool> RequestSignUp(SignUpDto signUpDto)
    {
        string url = "http://localhost:3000/sign-up";

        // create payload
        string payloadString = JsonSerializer.Serialize(signUpDto, _jsonCasing);
        HttpContent payload = new StringContent(payloadString, Encoding.UTF8, "application/json");

        // send
        HttpClient client = new HttpClient();
        var response = await client.PostAsync(url, payload);

        // failure!
        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            await Shell.Current.DisplayAlertAsync(
                "signup error!",
                $"{message}",
                "ok"
                );
            return false;
        }

        // success!
        return true;
    }
}
