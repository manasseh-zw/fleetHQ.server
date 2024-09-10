namespace FleetHQ.Server.Domains.Company;

public record CompanyProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public record CreateCompanyDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}