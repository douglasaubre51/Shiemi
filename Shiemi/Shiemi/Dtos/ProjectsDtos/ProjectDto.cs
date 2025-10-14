namespace Shiemi.Dtos.ProjectsDtos;

public class ProjectDto
{
    public string Title { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Profile { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

}
