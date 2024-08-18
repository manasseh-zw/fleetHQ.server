using FleetHQ.Server.Repository.Models;

using Microsoft.AspNetCore.Authorization;

namespace FleetHQ.Server.Authorization;

public class AccessControlAttribute(string feature, Access requiredAccess) : AuthorizeAttribute
{
    public string Feature { get; } = feature;
    public Access RequiredAccess { get; } = requiredAccess;
}

