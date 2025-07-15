using Shiemi.Dto;
using Shiemi.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Shiemi.Services;

public class UserService
{
    // di
    private readonly JsonSerializerOptions _jsonCasing;

    HttpClient _client;

    public UserService(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonCasing = jsonSerializerOptions;
        _client = new HttpClient();
    }

    // POST: sign-in
    public async Task<string?> RequestSignIn(SignInDto signIn)
    {
        string url = "http://localhost:3000/sign-in";

        string payloadString = JsonSerializer.Serialize(signIn, _jsonCasing);
        HttpContent content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        // send post request!
        HttpResponseMessage response = await _client.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            await Shell.Current.DisplayAlertAsync(
                "sign in error!",
                $"{message}",
                "try again"
            );
            return null;
        }

        // success
        // fetch user id
        string responseJsonString = await response.Content.ReadAsStringAsync();
        UserIdDto? dto = JsonSerializer.Deserialize<UserIdDto>(responseJsonString, _jsonCasing);
        string userId = dto.UserId;

        return userId;
    }

    // POST: sign-up
    public async Task<bool> RequestSignUp(SignUpDto signUpDto)
    {
        string url = "http://localhost:3000/sign-up";

        // create payload
        string payloadString = JsonSerializer.Serialize(signUpDto, _jsonCasing);
        HttpContent payload = new StringContent(payloadString, Encoding.UTF8, "application/json");

        // send a post request
        var response = await _client.PostAsync(url, payload);

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

    // GET: user-details/:user_id
    public async Task<Details?> RequestUserDetails(string userId)
    {
        string url = "http://localhost:3000/user-details/" + userId;

        HttpResponseMessage response;
        string jsonContent;

        // send a get request!
        response = await _client.GetAsync(url);
        jsonContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"{jsonContent}");
            return null;
        }

        // success
        var details = JsonSerializer.Deserialize<Details>(jsonContent, _jsonCasing);
        return details;
    }
}
