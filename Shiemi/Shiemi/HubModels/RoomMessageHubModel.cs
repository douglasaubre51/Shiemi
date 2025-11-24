namespace Shiemi.HubModels;

public class RoomMessageHubModel
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsOwner { get; set; }
    public int UserId { get; set; }
    public int ChannelId { get; set; }
    public int RoomId { get; set; }
}
