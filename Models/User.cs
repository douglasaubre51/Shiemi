using Shiemi.Attributes;

namespace Shiemi.Models
{
    public class User
    {
        [UserValidation]
        public string FirstName { get; set; }

        [UserValidation]
        public string LastName { get; set; }

        [UserValidation]
        public string Email { get; set; }

        [UserValidation]
        public string Password { get; set; }
    }
}
