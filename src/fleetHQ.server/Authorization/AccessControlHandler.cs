using System.Security.Claims;

using FleetHQ.Server.Repository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Authorization;

public class AccessControlHandler(RepositoryContext repository) : AuthorizationHandler<AccessControlRequirement>
{
    private readonly RepositoryContext _repository = repository;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, AccessControlRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roleId = context.User.FindFirst(ClaimTypes.Role)?.Value;

        if (userId == null || roleId == null)
        {
            context.Fail();
            return;
        }

        var userRole = await _repository.Users
        .AsNoTracking()
        .Where(u => u.Id == Guid.Parse(userId))
        .Select(x => x.Role).Include(x => x.Permissions)
        .FirstOrDefaultAsync();

        if (userRole == null)
        {
            context.Fail();
            return;
        }

        var permission = userRole.Permissions.FirstOrDefault(p => p.Feature == requirement.Feature);

        if (permission != null && permission.Access >= requirement.RequiredAccess)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

    }
}