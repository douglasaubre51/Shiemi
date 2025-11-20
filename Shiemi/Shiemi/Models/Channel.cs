namespace Shiemi.Models;

public class Channel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Profile { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    public string? PinnedMessage { get; set; }
    public List<Message>? Messages { get; set; }
}
