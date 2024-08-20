using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetHQ.Server.Domains.Vehicles;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "VehiclesView")]
    public IActionResult GetVehicles()
    {
        return Ok("GetVehicles Success");
    }

    [HttpPost]
    [Authorize(Policy = "VehiclesEdit")]
    public IActionResult AddVehicle(string vehicle)
    {
        return Ok("AddVehicle Success" + vehicle);
    }
}
