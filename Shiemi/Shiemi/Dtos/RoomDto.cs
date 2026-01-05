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
public class GetDevRoomDto
{
    public int RoomId { get; set; }
    public int ClientId { get; set; }

    public string ProfilePhotoURL { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public RoomTypes RoomType { get; set; } = RoomTypes.DEV;
}
