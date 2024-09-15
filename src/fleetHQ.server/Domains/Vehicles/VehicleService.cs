using FleetHQ.Server.Helpers;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Domains.Vehicles;

public interface IVehicleService
{
    Task<IXResult> AddVehicle(Guid companyId, AddVehicleDto dto);
    Task<IXResult> GetVehicles(Guid companyId);
    Task<IXResult> UpdateVehicle(Guid vehicleId, UpdateVehicleDto dto);
    Task<IXResult> DeleteVehicle(Guid vehicleId);
    Task<IXResult> DeleteVehicles(DeleteVehiclesDto dto);
}

public class VehicleService(RepositoryContext repository) : IVehicleService
{
    private readonly RepositoryContext _repository = repository;
    public async Task<IXResult> AddVehicle(Guid companyId, AddVehicleDto dto)
    {
        var companyExists = await _repository.Companies.AnyAsync(c => c.Id == companyId);

        if (!companyExists)
        {
            return XResult.Fail(["company not found"]);
        }

        var vehicle = new VehicleModel
        {
            Type = dto.Type,
            Year = dto.Year,
            Make = dto.Make,
            Model = dto.Model,
            LicensePlate = dto.LicensePlate,
            CompanyId = companyId
        };

        var validationResult = new VehicleValidator().Validate(vehicle);

        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        await _repository.Vehicles.AddAsync(vehicle);
        await _repository.SaveChangesAsync();

        var response = new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Make = vehicle.Make,
            Model = vehicle.Make,
            Type = vehicle.Type,
            Year = vehicle.Year,
            Seats = vehicle.Seats
        };

        return XResult.Ok(response, "Vehicle added!");
    }

    public async Task<IXResult> GetVehicles(Guid companyId)
    {
        var companyExists = await _repository.Companies.AnyAsync(c => c.Id == companyId);

        if (!companyExists)
        {
            return XResult.Fail(["company not found"]);
        }

        var vehicles = await _repository.Vehicles.Where(v => v.CompanyId == companyId).Select(v => new VehicleDto
        {
            Id = v.Id,
            LicensePlate = v.LicensePlate,
            Make = v.Make,
            Model = v.Model,
            Type = v.Type,
            Year = v.Year,
        }).ToListAsync();

        return XResult.Ok(vehicles);
    }

    public async Task<IXResult> UpdateVehicle(Guid vehicleId, UpdateVehicleDto dto)
    {
        var vehicle = await _repository.Vehicles.FindAsync(vehicleId);

        if (vehicle == null)
        {
            return XResult.Fail(["Vehicle not found"]);
        }

        vehicle.Type = dto.Type;
        vehicle.Year = dto.Year;
        vehicle.Make = dto.Make;
        vehicle.Model = dto.Model;
        vehicle.LicensePlate = dto.LicensePlate;

        var validationResult = new VehicleValidator().Validate(vehicle);

        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        await _repository.SaveChangesAsync();

        var response = new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Make = vehicle.Make,
            Model = vehicle.Make,
            Type = vehicle.Type,
            Year = vehicle.Year,
            Seats = vehicle.Seats
        };


        return XResult.Ok(response, "Vehicle updated successfully!");
    }

    public async Task<IXResult> DeleteVehicle(Guid vehicleId)
    {
        var vehicle = await _repository.Vehicles.FindAsync(vehicleId);

        if (vehicle == null)
        {
            return XResult.Fail(["Vehicle not found"]);
        }

        _repository.Vehicles.Remove(vehicle);
        await _repository.SaveChangesAsync();

        return XResult.Ok("", "Vehicle deleted successfully!");
    }

    public async Task<IXResult> DeleteVehicles(DeleteVehiclesDto dto)
    {
        var vehicles = await _repository.Vehicles
            .Where(v => dto.VehicleIds.Contains(v.Id))
            .ToListAsync();

        if (!vehicles.Any())
        {
            return XResult.Fail(["No vehicles found for deletion"]);
        }

        _repository.Vehicles.RemoveRange(vehicles);
        await _repository.SaveChangesAsync();

        return XResult.Ok("", $"{vehicles.Count} vehicles deleted successfully!");
    }
}