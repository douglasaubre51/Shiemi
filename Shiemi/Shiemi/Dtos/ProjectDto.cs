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

public record CreateProjectDto(
    string Title,
    string ShortDesc,
    string Description,
    int UserId
);