using System.ComponentModel.DataAnnotations;

namespace Shiemi.Models;

public class SignIn
{
    [Required]
    [EmailAddress(ErrorMessage = "not an email!")]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
