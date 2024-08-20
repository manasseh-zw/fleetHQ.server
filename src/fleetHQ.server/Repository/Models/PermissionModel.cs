using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetHQ.Server.Repository.Models;

public class PermissionModel
{
    [Key]
    public Guid Id { get; set; }
    public Access Access { get; set; }

    [ForeignKey(nameof(FeatureModel))]
    public Guid FeatureId { get; set; }
    public FeatureModel Feature { get; set; } = new();

    [ForeignKey(nameof(RoleModel))]
    public Guid RoleId { get; set; }
    public RoleModel Role { get; set; } = new();
}


public enum Access
{
    View,
    Edit
}