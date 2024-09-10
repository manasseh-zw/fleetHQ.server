using FleetHQ.Server.Repository.Models;

using FluentValidation;

using PhoneNumbers;

namespace FleetHQ.Server.Domains.Company;

public class CompanyValidator : AbstractValidator<CompanyModel>
{
    private const string NamePattern = @"^[a-zA-Z0-9]+$";
    private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();

    public CompanyValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .Matches(NamePattern).WithMessage("Name may contain letters and digits only");

        RuleFor(x => x.ContactNumber)
            .NotEmpty().WithMessage("Contact number must not be empty")
            .Must(BeAValidPhoneNumber).WithMessage("Contact number is be a valid Zimbabwean number");
    }

    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        try
        {
            var parsedNumber = PhoneUtil.Parse(phoneNumber, "ZW"); // Assuming US, change if needed
            return PhoneUtil.IsValidNumber(parsedNumber);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}