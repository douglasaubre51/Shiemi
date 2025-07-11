using System.ComponentModel.DataAnnotations;
using Shiemi.Dto;
using Shiemi.Models;

namespace Shiemi.Helpers;

public class SignUpValidator
{
    public async Task<SignUpValidatorDto?> Validate(User user)
    {
        // validate user
        var validationContext = new ValidationContext(user);
        var validationResults = new List<ValidationResult>();
        bool validationResult = Validator.TryValidateObject(
            user,
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
                .Where(e => e.MemberNames.Contains(nameof(User.FirstName)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in firstNameErrorMessage)
            {
                validatorDto.FirstNameMessage += m + "\n";
            }

            var lastNameErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(User.LastName)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in lastNameErrorMessage)
            {
                validatorDto.LastNameMessage += m + "\n";
            }

            var emailErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(User.Email)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in emailErrorMessage)
            {
                validatorDto.EmailMessage += m + "\n";
            }

            var phoneNoErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(User.PhoneNo)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in phoneNoErrorMessage)
            {
                validatorDto.PhoneNoMessage += m + "\n";
            }

            var passwordErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(User.Password)))
                .Select(e => e.ErrorMessage)
                .ToList();
            foreach (var m in passwordErrorMessage)
            {
                validatorDto.PasswordMessage += m + "\n";
            }

            var checkPasswordErrorMessage = validationResults
                .Where(e => e.MemberNames.Contains(nameof(User.ConfirmPassword)))
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