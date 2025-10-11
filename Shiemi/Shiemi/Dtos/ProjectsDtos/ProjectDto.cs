namespace Shiemi.Dtos.ProjectsDtos;

public class ProjectDto
{
    public string Title { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int UserId { get; set; }
}
