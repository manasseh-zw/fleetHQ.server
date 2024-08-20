using FleetHQ.Server.Repository.Models;

using Microsoft.AspNetCore.Authorization;

namespace FleetHQ.Server.Authorization;


public class PermissionRequirement(string feature, Permission permission) : IAuthorizationRequirement
{
    public (string feature, Permission permission) RequiredPermission { get; set; } = (feature, permission);
}
