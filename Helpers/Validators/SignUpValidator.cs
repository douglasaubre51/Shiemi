using Shiemi.Dto.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Shiemi.Helpers.Validators;

public class SignUpValidator
{
    public async Task<SignUpValidatorDto?> Validate(SignUpDto signUpDto)
    {
        // validate user
        var validationContext = new ValidationContext(signUpDto);
        var validationResults = new List<ValidationResult>();
        bool validationResult = Validator.TryValidateObject(
            signUpDto,
            validationContext,
            validationResults,
            true
            );

        // failure
        if (validationResult is false)
        {
            SignUpValidatorDto validatorDto = new();

            // collect validation errors
            var firstNameErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(SignUpDto.FirstName)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in firstNameErrorMessage)
            {
                validatorDto.FirstNameMessage += m + "\n";
            }

            var lastNameErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(SignUpDto.LastName)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in lastNameErrorMessage)
            {
                validatorDto.LastNameMessage += m + "\n";
            }

            var emailErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(SignUpDto.Email)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in emailErrorMessage)
            {
                validatorDto.EmailMessage += m + "\n";
            }

            var phoneNoErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(SignUpDto.PhoneNo)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in phoneNoErrorMessage)
            {
                validatorDto.PhoneNoMessage += m + "\n";
            }

            var passwordErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(SignUpDto.Password)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in passwordErrorMessage)
            {
                validatorDto.PasswordMessage += m + "\n";
            }

            var checkPasswordErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(SignUpDto.ConfirmPassword)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in checkPasswordErrorMessage)
            {
                validatorDto.CheckPasswordMessage += m + "\n";
            }

            await Shell.Current.DisplayAlertAsync(
                "validation error",
                "enter all fields properly!",
                "ok"
                );

            return validatorDto;
        }

        // success
        return null;
    }
}