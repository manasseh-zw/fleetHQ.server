using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetHQ.Server.Repository.Models;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public OnBoarding OnBoarding { get; set; }

    [ForeignKey(nameof(RoleModel))]
    public Guid RoleId { get; set; }
    public RoleModel Role { get; set; } = new();

    [ForeignKey(nameof(CompanyModel))]
    public Guid? CompanyId { get; set; }
    public CompanyModel? Company { get; set; } 

    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}
public enum OnBoarding
{
    account,
    Company,
    Vehicle,
    Complete
}
