using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Shiemi.Models;

namespace Shiemi.Helpers;

public class SignInValidator
{
    public bool Validate(SignIn model, out string emailValidation, out string passwordValidation)
    {
        // assign default values to out vars
        emailValidation = string.Empty;
        passwordValidation = string.Empty;

        // validation
        ValidationContext context = new ValidationContext(model);
        List<ValidationResult> results = new List<ValidationResult>();

        bool result = Validator.TryValidateObject(model, context, results, true);

        // invalid!
        if (!result)
        {
            // write to console
            foreach (var m in results)
            {
                Debug.WriteLine(m.ErrorMessage);
            }

            // write to labels
            // email validation label
            var emailValidationMessages = results
            .Where(e => e.MemberNames.Contains(nameof(SignIn.Email)))
            .Select(e => e.ErrorMessage)
            .ToList();
            foreach (var m in emailValidationMessages)
            {
                emailValidation += m + "\n";
            }

            // email validation label
            var passwordValidationMessages = results.
            Where(e => e.MemberNames.Contains(nameof(SignIn.Password)))
            .Select(e => e.ErrorMessage)
            .ToList();
            foreach (var m in passwordValidationMessages)
            {
                passwordValidation += m + "\n";
            }

            return false;
        }

        // on successful validation!
        return true;
    }
}
