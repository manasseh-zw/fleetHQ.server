using System.ComponentModel.DataAnnotations;
using System.Security;

namespace FleetHQ.Server.Repository.Models;

public class RoleModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PermissionModel> Permissions { get; set; } = [];
}

