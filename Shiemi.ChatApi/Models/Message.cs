using System.ComponentModel.DataAnnotations;

namespace Shiemi.ChatApi.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int SenderId { get; set; }
    }
}
