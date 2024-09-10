
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



}
