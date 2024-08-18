namespace FleetHQ.Server.Domains.Auth;

public record RegisterDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
}

public record LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}


public record AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
}