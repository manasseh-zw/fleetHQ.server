using FleetHQ.Server.Repository.Models;
using FluentValidation;

namespace FleetHQ.Server.Domains.Driver;

public class DriverValidator : AbstractValidator<DriverModel>
{
    public DriverValidator()
    {
        RuleFor(d => d.FullName).NotEmpty().MaximumLength(100);
        RuleFor(d => d.ContactNumber).NotEmpty().MaximumLength(20);
        RuleFor(d => d.Address).NotEmpty().MaximumLength(200);
        RuleFor(d => d.HireDate).NotEmpty().LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(d => d.CompanyId).NotEmpty();
    }
}