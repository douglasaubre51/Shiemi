namespace Shiemi.Models;

public class Review
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProjectId { get; set; }

    public string UserName { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
