namespace FleetHQ.Server.Repository.Models;

public class CompanyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
    public List<VehicleModel> Vehicles { get; set; } = [];
    public List<DriverModel> Drivers { get; set; } = [];
    public List<UserModel> Users { get; set; } = [];

}