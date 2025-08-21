using System.ComponentModel.DataAnnotations;

namespace Shiemi.ChatApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProfilePhoto { get; set; } = string.Empty;
        public bool IsDeveloper { get; set; }

        public List<Project>? CreatedProjects { get; set; }
        public List<Project>? JoinedProjects { get; set; }
    }
}
