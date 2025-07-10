using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Shiemi.Models;

namespace Shiemi.Helpers;

public class SignInValidator
{
    public bool Validate(SignIn model)
    {
        ValidationContext context = new ValidationContext(model);
        List<ValidationResult> results = new List<ValidationResult>();

        bool result = Validator.TryValidateObject(model, context, results, true);

        if (!result)
        {
            foreach (var m in results)
            {
                Debug.WriteLine(m.ErrorMessage);
            }

            return false;
        }

        return true;
    }
}
