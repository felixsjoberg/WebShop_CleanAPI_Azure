using FluentValidation;
namespace Application.Authentication.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty()
        .Must(password => ValidatePassword(password))
            .WithMessage("Passwords must have at least one non-alphanumeric character, one digit, and one uppercase letter.");
    }

    private bool ValidatePassword(string password)
    {
        // Check if the password meets the requirements
        var hasNonAlphanumeric = password.Any(char.IsLetterOrDigit) && password.All(ch => !char.IsLetterOrDigit(ch));
        var hasDigit = password.Any(char.IsDigit);
        var hasUpperCase = password.Any(char.IsUpper);

        return hasNonAlphanumeric && hasDigit && hasUpperCase;
    }
}
