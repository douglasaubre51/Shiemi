namespace Shiemi.ViewModels;

public record MessageViewModel(
    int Id,
    string Text,
    DateTime CreatedAt,
    int UserId,
    bool IsOwner,
    int ChannelId,
    int RoomId
);
