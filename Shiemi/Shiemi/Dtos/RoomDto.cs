using Shiemi.Models;

namespace Shiemi.Dtos;

public record RoomDto(
    int Id,
    int OwnerId,
    int TenantId,
    int ProjectId,
    List<Message>? Messages
);

public record GetPrivateRoomDto(
    int UserId,
    int ProjectId,
	int DevId,
    RoomTypes RoomType
);
