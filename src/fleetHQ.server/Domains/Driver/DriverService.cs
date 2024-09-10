using FleetHQ.Server.Helpers;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Domains.Driver;

public interface IDriverService
{
    Task<IXResult> AddDriver(Guid companyId, AddDriverDto dto);
    Task<IXResult> GetDrivers(Guid companyId);
    Task<IXResult> UpdateDriver(Guid driverId, UpdateDriverDto dto);
    Task<IXResult> DeleteDriver(Guid driverId);
}

public class DriverService(RepositoryContext repository) : IDriverService
{
    private readonly RepositoryContext _repository = repository;

    public async Task<IXResult> AddDriver(Guid companyId, AddDriverDto dto)
    {
        var companyExists = await _repository.Companies.AnyAsync(c => c.Id == companyId);

        if (!companyExists)
        {
            return XResult.Fail(["Company not found"]);
        }

        var driver = new DriverModel
        {
            FullName = dto.FullName,
            ContactNumber = dto.ContactNumber,
            Address = dto.Address,
            HireDate = dto.HireDate,
            CompanyId = companyId
        };

        var validationResult = new DriverValidator().Validate(driver);

        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        await _repository.Drivers.AddAsync(driver);
        await _repository.SaveChangesAsync();

        return XResult.Ok("", "Driver added successfully!");
    }

    public async Task<IXResult> GetDrivers(Guid companyId)
    {
        var companyExists = await _repository.Companies.AnyAsync(c => c.Id == companyId);

        if (!companyExists)
        {
            return XResult.Fail(["Company not found"]);
        }

        var drivers = await _repository.Drivers
            .Where(d => d.CompanyId == companyId)
            .Select(d => new DriverDto
            {
                Id = d.Id,
                FullName = d.FullName,
                ContactNumber = d.ContactNumber,
                Address = d.Address,
                HireDate = d.HireDate,
                VehicleId = d.VehicleId
            })
            .ToListAsync();

        return XResult.Ok(drivers);
    }

    public async Task<IXResult> UpdateDriver(Guid driverId, UpdateDriverDto dto)
    {
        var driver = await _repository.Drivers.FindAsync(driverId);

        if (driver == null)
        {
            return XResult.Fail(["Driver not found"]);
        }

        driver.FullName = dto.FullName;
        driver.ContactNumber = dto.ContactNumber;
        driver.Address = dto.Address;
        driver.HireDate = dto.HireDate;

        var validationResult = new DriverValidator().Validate(driver);

        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        await _repository.SaveChangesAsync();

        return XResult.Ok("", "Driver updated successfully!");
    }

    public async Task<IXResult> DeleteDriver(Guid driverId)
    {
        var driver = await _repository.Drivers.FindAsync(driverId);

        if (driver == null)
        {
            return XResult.Fail(["Driver not found"]);
        }

        _repository.Drivers.Remove(driver);
        await _repository.SaveChangesAsync();

        return XResult.Ok("", "Driver deleted successfully!");
    }
}