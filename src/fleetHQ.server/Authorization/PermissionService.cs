namespace FleetHQ.Server.Authorization;

using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid roleId, string feature, Permission requiredPermission);

}

public class PermissionService(RepositoryContext repository) : IPermissionService
{
    private readonly RepositoryContext _repository = repository;
    public async Task<bool> HasPermissionAsync(Guid roleId, string feature, Permission requiredPermission)
    {
        var userPermissions = await _repository.Roles
         .AsNoTracking()
         .Where(r => r.Id == roleId)
         .Select(r => r.Permissions).FirstOrDefaultAsync();


        if (userPermissions == null)
        {
            return false;
        }

        foreach (var permissionSet in userPermissions)
        {
            if (permissionSet.TryGetValue(feature, out var userPermission))
            {
                if (userPermission >= requiredPermission)
                {
                    return true;
                }
            }
        }

        return false;
    }
}