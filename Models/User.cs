using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shiemi.Models
{
    public class User : ObservableValidator
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "not an email!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "passwords must match!")]
        [JsonIgnore]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "not a valid phone no!", MinimumLength = 10)]
        public string PhoneNo { get; set; }
    }
}
