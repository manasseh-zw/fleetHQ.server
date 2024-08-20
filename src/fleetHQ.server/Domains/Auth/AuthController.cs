using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetHQ.Server.Domains.Auth;


[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController(IAuthService service) : ControllerBase
{
    private readonly IAuthService _service = service;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _service.Register(dto);
        return !result.IsSuccess ? BadRequest(result) : Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _service.Login(dto);
        return !result.IsSuccess ? BadRequest(result) : Ok(result);
    }

}