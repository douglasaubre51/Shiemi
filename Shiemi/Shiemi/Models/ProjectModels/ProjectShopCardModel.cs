namespace Shiemi.Models.ProjectModels;

public class ProjectShopCardModel
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public string UserProfilePhoto { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string Short { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
}
