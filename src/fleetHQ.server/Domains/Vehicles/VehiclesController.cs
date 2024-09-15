using System.Security.Claims;

using FleetHQ.Server.Repository.Models;
using FleetHQ.Server.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace FleetHQ.Server.Domains.Vehicles;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class VehiclesController(IVehicleService service) : ControllerBase
{

    private readonly IVehicleService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetVehicles()
    {
        var companyId = User.FindFirst(Constants.CompanyId)?.Value;
        if (companyId == null) return Unauthorized();

        var result = await _service.GetVehicles(Guid.Parse(companyId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);

    }

    [HttpPost]
    public async Task<IActionResult> AddVehicle([FromBody] AddVehicleDto dto)
    {
        var companyId = User.FindFirst(Constants.CompanyId)?.Value;
        if (companyId == null) return Unauthorized();

        var result = await _service.AddVehicle(Guid.Parse(companyId), dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{vehicleId}")]
    public async Task<IActionResult> UpdateVehicle(Guid vehicleId, [FromBody] UpdateVehicleDto dto)
    {
        var result = await _service.UpdateVehicle(vehicleId, dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{vehicleId}")]
    public async Task<IActionResult> DeleteVehicle(Guid vehicleId)
    {
        var result = await _service.DeleteVehicle(vehicleId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("delete-bulk")]
    public async Task<IActionResult> DeleteVehicles([FromBody] DeleteVehiclesDto dto)
    {
        var result = await _service.DeleteVehicles(dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
