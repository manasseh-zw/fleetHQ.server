using FluentValidation;

namespace FleetHQ.Server.Domains.Auth;

public class AuthValidator : AbstractValidator<RegisterDto>
{
    public AuthValidator()
    {
        RuleFor(u => u.Password)
        .NotEmpty().WithMessage("Password must not be empty")
        .MinimumLength(6).WithMessage("Password must be at least 6 characters")
        .Matches(@"^(?=.*[A-Z])(?=.*[^a-zA-Z0-9\s]).+$")
        .WithMessage("Password must have at least one uppercase and a special character without spaces");

        RuleFor(u => u.Email)
        .NotEmpty().WithMessage("Email must not be empty")
        .EmailAddress().WithMessage("Invalid email address");
    }
}