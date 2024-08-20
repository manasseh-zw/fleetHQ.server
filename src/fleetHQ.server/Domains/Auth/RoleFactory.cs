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
                    Feature = new()
                    {
                        Name = AppFeatures.Vehicles
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Drivers,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Bookings,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Expenses,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Service,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Places,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Documents,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.FuelUsage,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Settings,
                    },
                    Access = Access.Edit,
                },
                new PermissionModel()
                {
                    Feature = new(){
                      Name = AppFeatures.Reports,
                    },
                    Access = Access.Edit,
                }
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