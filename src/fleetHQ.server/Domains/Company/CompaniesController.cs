using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

namespace FleetHQ.Server.Domains.Company;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController(ICompanyService service) : ControllerBase
{
    private readonly ICompanyService _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var result = await _service.Create(Guid.Parse(userId), dto);

        return !result.IsSuccess ? BadRequest(result) : Ok(result);
    }
}