using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetHQ.Server.Repository.Models;

public class PermissionModel
{
    [Key]
    public Guid Id { get; set; }
    public string Feature { get; set; } = string.Empty;
    public Access Access { get; set; }

    [ForeignKey(nameof(RoleModel))]
    public Guid RoleId { get; set; }
    public RoleModel Role { get; set; } = new();
}

public static class Features
{
    public const string Vehicles = "Vehicles";
    public const string Drivers = "Drivers";
    public const string Bookings = "Bookings";
    public const string Expenses = "Expenses";
    public const string Reports = "Reports";
    public const string Places = "Places";
    public const string FuelUsage = "FuelUsage";
    public const string Documents = "Documents";
    public const string Settings = "Settings";

    public static readonly List<string> All =
    [
        Vehicles, Drivers, Bookings, Expenses, Reports,
        Places, FuelUsage, Documents, Settings
    ];
}

public enum Access
{

    View,
    Edit
}