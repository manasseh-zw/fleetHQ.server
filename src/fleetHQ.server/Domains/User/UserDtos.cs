using FleetHQ.Server.Domains.Company;
using FleetHQ.Server.Repository.Models;
namespace FleetHQ.Server.Domains.User;

public record UserProfileDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public RoleDto Role { get; set; } = new();
    public OnBoarding OnBoarding { get; set; }
    public CompanyProfileDto? Company { get; set; }

}

public record RoleDto
{
    public string Name { get; set; } = string.Empty;
    public List<Dictionary<string, Permission>> Permissions { get; set; } = [];
}