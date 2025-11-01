using Shiemi.Models;

namespace Shiemi.Dtos.RoomDtos;

public class RoomDto
{
    public int Id { get; set; }

    public int OwnerId { get; set; }
    public int TenantId { get; set; }
    public int ProjectId { get; set; }

    public List<Message>? Messages { get; set; }
}
