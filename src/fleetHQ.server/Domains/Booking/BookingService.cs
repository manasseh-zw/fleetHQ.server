using FleetHQ.Server.Helpers;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Domains.Booking;

public interface IBookingService
{
    Task<IXResult> AddBooking(Guid companyId, AddBookingDto dto);
    Task<IXResult> GetBookings(Guid companyId);
    Task<IXResult> UpdateBooking(Guid bookingId, UpdateBookingDto dto);
    Task<IXResult> DeleteBooking(Guid bookingId);
    Task<IXResult> AssignDriverAndVehicle(Guid bookingId, Guid driverId, Guid vehicleId);
}

public class BookingService : IBookingService
{
    private readonly RepositoryContext _repository;

    public BookingService(RepositoryContext repository)
    {
        _repository = repository;
    }

    public async Task<IXResult> AddBooking(Guid companyId, AddBookingDto dto)
    {
        var booking = new BookingModel
        {
            CustomerName = dto.CustomerName,
            CustomerContact = dto.CustomerContact,
            CustomerLocation = dto.CustomerLocation,
            CustomerDestination = dto.CustomerDestination,
            PassengerCount = dto.PassengerCount,
            Time = dto.Time,
            CompanyId = companyId
        };

        if (dto.DriverId.HasValue && dto.VehicleId.HasValue)
        {
            var assignResult = await AssignDriverAndVehicleInternal(booking, dto.DriverId.Value, dto.VehicleId.Value);
            if (!assignResult.IsSuccess)
            {
                return assignResult;
            }
        }
        else if (dto.DriverId.HasValue || dto.VehicleId.HasValue)
        {
            return XResult.Fail(["Both driver and vehicle must be assigned together"]);
        }

        var validationResult = new BookingValidator().Validate(booking);
        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        await _repository.Bookings.AddAsync(booking);
        await _repository.SaveChangesAsync();

        return XResult.Ok("", "Booking added successfully!");
    }

    public async Task<IXResult> GetBookings(Guid companyId)
    {
        var bookings = await _repository.Bookings
            .Where(b => b.CompanyId == companyId)
            .Select(b => new BookingDto
            {
                Id = b.Id,
                CustomerName = b.CustomerName,
                CustomerContact = b.CustomerContact,
                CustomerLocation = b.CustomerLocation,
                CustomerDestination = b.CustomerDestination,
                PassengerCount = b.PassengerCount,
                Time = b.Time,
                DriverId = b.DriverId,
                VehicleId = b.VehicleId
            })
            .ToListAsync();

        return XResult.Ok(bookings);
    }

    public async Task<IXResult> UpdateBooking(Guid bookingId, UpdateBookingDto dto)
    {
        var booking = await _repository.Bookings.FindAsync(bookingId);
        if (booking == null)
        {
            return XResult.Fail(["Booking not found"]);
        }

        booking.CustomerName = dto.CustomerName;
        booking.CustomerContact = dto.CustomerContact;
        booking.CustomerLocation = dto.CustomerLocation;
        booking.CustomerDestination = dto.CustomerDestination;
        booking.PassengerCount = dto.PassengerCount;
        booking.Time = dto.Time;

        var validationResult = new BookingValidator().Validate(booking);
        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        await _repository.SaveChangesAsync();

        return XResult.Ok("", "Booking updated successfully!");
    }

    public async Task<IXResult> DeleteBooking(Guid bookingId)
    {
        var booking = await _repository.Bookings.FindAsync(bookingId);
        if (booking == null)
        {
            return XResult.Fail(["Booking not found"]);
        }

        _repository.Bookings.Remove(booking);
        await _repository.SaveChangesAsync();

        return XResult.Ok("", "Booking deleted successfully!");
    }

    public async Task<IXResult> AssignDriverAndVehicle(Guid bookingId, Guid driverId, Guid vehicleId)
    {
        var booking = await _repository.Bookings.FindAsync(bookingId);
        if (booking == null)
        {
            return XResult.Fail(["Booking not found"]);
        }

        var result = await AssignDriverAndVehicleInternal(booking, driverId, vehicleId);
        if (result.IsSuccess)
        {
            await _repository.SaveChangesAsync();
        }

        return result;
    }

    private async Task<IXResult> AssignDriverAndVehicleInternal(BookingModel booking, Guid driverId, Guid vehicleId)
    {
        var driver = await _repository.Drivers.FindAsync(driverId);
        if (driver == null)
        {
            return XResult.Fail(["Driver not found"]);
        }

        var vehicle = await _repository.Vehicles.FindAsync(vehicleId);
        if (vehicle == null)
        {
            return XResult.Fail(["Vehicle not found"]);
        }

        if (vehicle.Seats < booking.PassengerCount)
        {
            return XResult.Fail(["Vehicle does not have enough capacity for this booking"]);
        }

        if (driver.CompanyId != booking.CompanyId || vehicle.CompanyId != booking.CompanyId)
        {
            return XResult.Fail(["Driver or vehicle does not belong to the same company as the booking"]);
        }

        booking.DriverId = driverId;
        booking.VehicleId = vehicleId;

        driver.VehicleId = vehicleId;

        var validationResult = new BookingValidator().Validate(booking);
        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        return XResult.Ok("", "Driver and vehicle assigned to booking successfully!");
    }
}
