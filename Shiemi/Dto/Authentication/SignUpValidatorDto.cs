namespace Shiemi.Dto.Authentication;

public class SignUpValidatorDto
{
    public string? FirstNameMessage { get; set; }
    public string? LastNameMessage { get; set; }
    public string? EmailMessage { get; set; }
    public string? PhoneNoMessage { get; set; }
    public string? PasswordMessage { get; set; }
    public string? CheckPasswordMessage { get; set; }

    public void Deconstruct(
        out string FirstNameMessage,
        out string LastNameMessage,
        out string EmailMessage,
        out string PhoneNoMessage,
        out string PasswordMessage,
        out string CheckPasswordMessage
        )
    {
        FirstNameMessage = this.FirstNameMessage ?? string.Empty;
        LastNameMessage = this.LastNameMessage ?? string.Empty;
        EmailMessage = this.EmailMessage ?? string.Empty;
        PhoneNoMessage = this.PhoneNoMessage ?? string.Empty;
        PasswordMessage = this.PasswordMessage ?? string.Empty;
        CheckPasswordMessage = this.CheckPasswordMessage ?? string.Empty;
    }
}
