using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Shiemi.Attributes
{
    public class UserValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string text = value as string;

            Debug.WriteLine($"validating : {text}");

            if (string.IsNullOrWhiteSpace(text)) return false;

            return true;
        }
    }
}
