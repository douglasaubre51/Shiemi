using System.ComponentModel.DataAnnotations;

namespace Shiemi.ChatApi.Models
{
    public class WaitingRoom
    {
        [Key]
        public int Id { get; set; }

        public List<Message>? Messages { get; set; }
        public User? NewUser { get; set; }
    }
}
