using FleetHQ.Server.Authorization;
using FleetHQ.Server.Repository.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetHQ.Server.Domains.Vehicles;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "Vehicles.Read")]
    public IActionResult GetVehicles()
    {
        return Ok("GetVehicles Success");
    }

    [HttpPost]
    [Authorize(Policy = "Vehicles.Write")]
    public IActionResult AddVehicle(string vehicle)
    {
        return Ok("AddVehicle Success" + vehicle);
    }
}
