using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace FleetHQ.Server.Authorization;

public class PermissionsAuthorizationHandler(IPermissionService permissionService) : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService = permissionService;
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roleId = context.User.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
        {
            return;
        }

        var hasRequiredPermissions = await _permissionService.HasPermissionAsync(Guid.Parse(roleId), requirement.RequiredPermission.feature, requirement.RequiredPermission.permission);


        if (hasRequiredPermissions)
        {
            context.Succeed(requirement);
        }

        return;
    }
}

public class PermissionAuthorizationRequirement : IAuthorizationRequirement;
