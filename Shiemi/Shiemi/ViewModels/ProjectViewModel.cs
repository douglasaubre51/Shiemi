namespace Shiemi.ViewModels;

public record ProjectViewModel(
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

public record ChatListProjectViewModel(
    int Id,
    string Title,
    int ChannelId
);

public record ProjectsPageProjectViewModel(
    int Id,
    string Title,
    string ShortDesc,
    string Description,
    string CreatedAt,
    int UserId
);
