namespace Shiemi.Models;

public class Message
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public int ChannelId { get; set; }
    public bool IsOwner { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; } = string.Empty;
}
