using System.ComponentModel.DataAnnotations;

namespace Shiemi.Dto.Authentication;

public class SignInDto
{
    [Required]
    [EmailAddress(ErrorMessage = "not an email!")]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
