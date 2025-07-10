using System.ComponentModel.DataAnnotations;

namespace Shiemi.Models;

public class SignIn
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
