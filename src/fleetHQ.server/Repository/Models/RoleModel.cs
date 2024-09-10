using System.ComponentModel.DataAnnotations;
namespace FleetHQ.Server.Repository.Models;

public class RoleModel
{
    
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Dictionary<string, Permission>> Permissions { get; set; } = [];
}

public enum Permission
{
    Read,
    Write
}

public struct Features
{
    public const string Vehicles = "Vehicles";
    public const string Drivers = "Drivers";
    public const string Bookings = "Bookings";
    public const string Expenses = "Expenses";
    public const string Reports = "Reports";
    public const string Places = "Places";
    public const string FuelUsage = "FuelUsage";
    public const string Service = "Service";
    public const string Documents = "Documents";
    public const string Settings = "Settings";

    public static readonly List<string> All =
    [
        Vehicles, Drivers, Bookings, Expenses, Reports,
        Places, FuelUsage, Service, Documents, Settings
    ];
}