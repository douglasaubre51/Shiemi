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

public record SendMessageDto(
    string Text,
    DateTime CreatedAt,
    int UserId,
    int ChannelId,
    int RoomId
);
