namespace Shiemi.Dto.Authentication;

public class SignUpValidatorDto
{
    public string FirstNameMessage { get; set; }
    public string LastNameMessage { get; set; }
    public string EmailMessage { get; set; }
    public string PhoneNoMessage { get; set; }
    public string PasswordMessage { get; set; }
    public string CheckPasswordMessage { get; set; }

    public void Deconstruct(
        out string FirstNameMessage,
        out string LastNameMessage,
        out string EmailMessage,
        out string PhoneNoMessage,
        out string PasswordMessage,
        out string CheckPasswordMessage
        )
    {
        FirstNameMessage = this.FirstNameMessage;
        LastNameMessage = this.LastNameMessage;
        EmailMessage = this.EmailMessage;
        PhoneNoMessage = this.PhoneNoMessage;
        PasswordMessage = this.PasswordMessage;
        CheckPasswordMessage = this.CheckPasswordMessage;
    }
}
