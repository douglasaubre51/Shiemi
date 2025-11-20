using Shiemi.Models;

namespace Shiemi.Dtos;

public record RoomDto(
    int Id,
    int OwnerId,
    int TenantId,
    int ProjectId,
    List<Message>? Messages
);
