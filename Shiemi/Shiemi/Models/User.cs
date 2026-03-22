namespace Shiemi.Models;

public class User
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long? Phone { get; set; }

    public bool IsDeveloper { get; set; }
    public bool IsAdmin { get; set; }

    public Photo? ProfilePhoto { get; set; }
    public List<Project>? Projects { get; set; }
}

public record ProfileCardModel(
    int Id,
    string Username,
    string ProfilePhotoURI,
    int RoomId
);

public class OptionalUserDetails
{
    public string Contact { get; set; } = string.Empty;
    public string Whatsaap { get; set; } = string.Empty;
    public string LinkedIn { get; set; } = string.Empty;
    public string Gmail { get; set; } = string.Empty;
    public string Github { get; set; } = string.Empty;
    public string AboutMe { get; set; } = string.Empty;
}