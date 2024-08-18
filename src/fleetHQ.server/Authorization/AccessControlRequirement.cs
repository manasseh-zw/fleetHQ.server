using FleetHQ.Server.Repository.Models;

using Microsoft.AspNetCore.Authorization;

namespace FleetHQ.Server.Authorization;

public class AccessControlRequirement(string feature, Access requiredAccess) : IAuthorizationRequirement
{
    public string Feature { get; } = feature;
    public Access RequiredAccess { get; } = requiredAccess;
}