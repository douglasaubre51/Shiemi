using System.ComponentModel.DataAnnotations;

namespace Shiemi.ChatApi.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Cost { get; set; }
        public DateOnly CreatedAt { get; set; }

        public User? PostedBy { get; set; }
        public Channel? Channel { get; set; }
    }
}
