using System.ComponentModel.DataAnnotations;

namespace Shiemi.ChatApi.Models
{
    public class Channel
    {
        [Key]
        public int Id { get; set; }

        public List<User>? JoinedUsers { get; set; }
        public List<int>? BlockList { get; set; }

        public Main? Main { get; set; }
        public List<PrivateRoom>? PrivateRooms { get; set; }
        public List<WaitingRoom>? WaitingRooms { get; set; }
    }
}
