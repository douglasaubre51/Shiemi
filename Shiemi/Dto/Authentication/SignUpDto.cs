using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shiemi.Dto.Authentication
{
    public class SignUpDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "not an email!")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(Password), ErrorMessage = "passwords must match!")]
        [JsonIgnore]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        [StringLength(10, ErrorMessage = "not a valid phone no!", MinimumLength = 10)]
        public string PhoneNo { get; set; } = string.Empty;
        public string ProfilePhoto { get; set; } = string.Empty;
    }
}
