using Shiemi.Dto;
using Shiemi.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
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
    public async Task<bool> RequestSignUp(SignUpDto signUpDto, string imageSource)
    {
        string url = "http://localhost:3000/sign-up";

        // create form
        var form = new MultipartFormDataContent();
        form.Add(new StringContent(signUpDto.FirstName), "firstName");
        form.Add(new StringContent(signUpDto.LastName), "lastName");
        form.Add(new StringContent(signUpDto.Email), "email");
        form.Add(new StringContent(signUpDto.Password), "password");
        form.Add(new StringContent(signUpDto.PhoneNo), "phoneNo");

        var imageForm = new ByteArrayContent(await File.ReadAllBytesAsync(imageSource));
        var imageExtension = Path.GetExtension(imageSource);
        string mimeType = imageExtension.ToLower() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream"
        };
        imageForm.Headers.ContentType = MediaTypeHeaderValue.Parse(mimeType);
        form.Add(imageForm, "profilePhoto", Path.GetFileName(imageSource));

        // send a post request with form
        var response = await _client.PostAsync(url, form);

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
