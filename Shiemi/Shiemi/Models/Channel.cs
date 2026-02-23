namespace Shiemi.Models;

public class Channel
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int OwnerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PinnedMessage { get; set; } = string.Empty;
    public List<Message>? Messages { get; set; }
}
