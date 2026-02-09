namespace Shiemi.Models.ProjectModels;

public class DeveloperCardModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ProfilePhotoURL { get; set; } = string.Empty;
}
