using FleetHQ.Server.Repository.Models;
namespace FleetHQ.Server.Domains.User;

public record UserProfileDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public RoleDto Role { get; set; } = new();
    public OnBoarding OnBoarding { get; set; }
}

public record RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Dictionary<string, Access>> Permissions { get; set; } = [];

}