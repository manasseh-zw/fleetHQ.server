using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetHQ.Server.Repository.Models;

public class DriverModel
{
    [Key]
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(CompanyModel))]
    public Guid CompanyId { get; set; }
    public CompanyModel Company { get; set; } = new();

    [ForeignKey(nameof(VehicleModel))]
    public Guid? VehicleId { get; set; }
    public VehicleModel? Vehicle { get; set; }

    public List<BookingModel>? Bookings { get; set; }

}