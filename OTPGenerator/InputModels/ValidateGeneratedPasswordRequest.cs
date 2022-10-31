namespace OTPGenerator.InputModels;

public class ValidateGeneratedPasswordRequest
{
    public string? UserId { get; set; }

    public string? OneTimePassword { get; set; }
}