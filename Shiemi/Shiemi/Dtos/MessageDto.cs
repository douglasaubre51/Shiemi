namespace Shiemi.Dtos;

public record MessageDto(
    int Id,
    string Text,
    DateTime CreatedAt,
    int UserId,
    bool IsOwner,
    int ChannelId,
    int RoomId
);
