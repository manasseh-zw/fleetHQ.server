using FleetHQ.Server.Repository.Models;
using FluentValidation;

namespace FleetHQ.Server.Domains.Booking;

public class BookingValidator : AbstractValidator<BookingModel>
{
    public BookingValidator()
    {
        RuleFor(b => b.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.");

        RuleFor(b => b.CustomerContact)
            .NotEmpty().WithMessage("Customer contact is required.");

        RuleFor(b => b.CustomerLocation)
            .NotEmpty().WithMessage("Customer location is required.");

        RuleFor(b => b.CustomerDestination)
            .NotEmpty().WithMessage("Customer destination is required.");

        RuleFor(b => b.PassengerCount)
            .GreaterThan(0).WithMessage("Passenger count must be greater than 0.")
            .LessThanOrEqualTo(50).WithMessage("Passenger count must not exceed 50.");

        RuleFor(b => b.Time)
            .NotEmpty().WithMessage("Booking time is required.")
            .GreaterThan(DateTime.UtcNow).WithMessage("Booking time must be in the future.");

        RuleFor(b => b.DriverId)
            .NotEmpty().WithMessage("Driver is required.")
            .When(b => b.VehicleId.HasValue);

        RuleFor(b => b.VehicleId)
            .NotEmpty().WithMessage("Vehicle is required.")
            .When(b => b.DriverId.HasValue);
    }
}
