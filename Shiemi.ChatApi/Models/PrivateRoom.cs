using System.ComponentModel.DataAnnotations;

namespace Shiemi.ChatApi.Models
{
    public class PrivateRoom
    {
        [Key]
        public int Id { get; set; }

        public List<Message>? Messages { get; set; }
        public List<Message>? PinnedMessages { get; set; }
        public User? Employee { get; set; }
    }
}
