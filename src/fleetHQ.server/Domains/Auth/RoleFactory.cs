using FleetHQ.Server.Repository.Models;

public static class RoleFactory
{
    public static RoleModel Manager()
    {
        return new()
        {
            Name = "Manager",
            Permissions = [
                new PermissionModel()
                {
                    Feature = Features.Settings,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.Bookings,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.Expenses,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.Drivers,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.Documents,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.FuelUsage,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.Places,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.Vehicles,
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = Features.Vehicles,
                    Access = Access.Edit,
                },
            ]
        };
    }

    public static RoleModel Default()
    {
        return new()
        {
            Name = "default",
            Permissions = []
        };
    }
}