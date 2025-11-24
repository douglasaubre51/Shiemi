namespace Shiemi.ViewModels;

public record MessageViewModel(
    int Id,
    string Text,
    DateTime CreatedAt,
    bool IsOwner,
    int UserId,
    int ChannelId,
    int RoomId
);
