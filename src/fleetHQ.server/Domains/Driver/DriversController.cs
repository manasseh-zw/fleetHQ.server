using System.Security.Claims;
using FleetHQ.Server.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetHQ.Server.Domains.Driver;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class DriversController : ControllerBase
{
    private readonly IDriverService _service;

    public DriversController(IDriverService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var companyId = User.FindFirst(Constants.CompanyId)?.Value;
        if (companyId == null) return Unauthorized();

        var result = await _service.GetDrivers(Guid.Parse(companyId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddDriver([FromBody] AddDriverDto dto)
    {
        var companyId = User.FindFirst(Constants.CompanyId)?.Value;
        if (companyId == null) return Unauthorized();

        var result = await _service.AddDriver(Guid.Parse(companyId), dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{driverId}")]
    public async Task<IActionResult> UpdateDriver(Guid driverId, [FromBody] UpdateDriverDto dto)
    {
        var result = await _service.UpdateDriver(driverId, dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{driverId}")]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        var result = await _service.DeleteDriver(driverId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}