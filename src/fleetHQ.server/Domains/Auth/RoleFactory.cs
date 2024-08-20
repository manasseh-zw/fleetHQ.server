namespace FleetHQ.Server.Domains.Auth;
using FleetHQ.Server.Repository.Models;

public static class RoleFactory
{
  public static RoleModel Manager()
  {
    return new RoleModel
    {
      Name = "Manager",
      Permissions =
            [
                new() { { Features.Vehicles, Permission.Write } },
                new() { { Features.Drivers, Permission.Write } },
                new() { { Features.Bookings, Permission.Write } },
                new() { { Features.Expenses, Permission.Write } },
                new() { { Features.Service, Permission.Write } },
                new() { { Features.Places, Permission.Write } },
                new() { { Features.Documents, Permission.Write } },
                new() { { Features.FuelUsage, Permission.Write } },
                new() { { Features.Settings, Permission.Write } },
                new() { { Features.Reports, Permission.Write } }
            ]
    };
  }

  public static RoleModel Default()
  {
    return new RoleModel
    {
      Name = "Default",
      Permissions = []
    };
  }
}