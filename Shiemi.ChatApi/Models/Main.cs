using System.ComponentModel.DataAnnotations;

namespace Shiemi.ChatApi.Models
{
    public class Main
    {
        [Key]
        public int Id { get; set; }

        public List<Message>? Messages { get; set; }
        public List<Message>? PinnedMessages { get; set; }
    }
}
