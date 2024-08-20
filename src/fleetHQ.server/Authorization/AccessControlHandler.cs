using System.Security.Claims;

using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

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

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
        {
            return;
        }

        var userRole = await _repository.Users
       .AsNoTracking()
       .Where(u => u.Id == Guid.Parse(userId))
       .Select(x => new
       {
           x.Role.Permissions
       })
       .FirstOrDefaultAsync();

        if (userRole?.Permissions == null)
        {
            return;
        }

        var permission = userRole.Permissions.FirstOrDefault(p => p.Feature.Name == requirement.Feature);

        if (permission != null && ((requirement.RequiredAccess == Access.View &&
            (permission.Access == Access.View || permission.Access == Access.Edit)) ||
             (requirement.RequiredAccess == Access.Edit && permission.Access == Access.Edit)))
        {
            context.Succeed(requirement);
        }

    }
}