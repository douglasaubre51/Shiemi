namespace Shiemi.Dtos;

public record ProjectDto(
    int Id,
    string Title,
    string ShortDesc,
    string Description,
    string CreatedAt,
    decimal Cost,
    int UserId,
    int ChannelId,
    List<int> PrivateRooms,
    List<int> UserList,
    List<int> BlockList
);

public class ProjectShopDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ChannelId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserProfilePhoto { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}

public record CreateProjectDto(
    string Title,
    string ShortDesc,
    string Description,
    int UserId
);

public record SearchProjectsDto(
    int Id,
    string Title,
    string? ShortDesc,
    int UserId
    );

public record EditProjectDto(
    int Id,
    string Title,
    string ShortDesc,
    string Description,
    List<string> Tags 
    );