using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetHQ.Server.Repository.Models;

public class VehicleModel
{
    [Key]
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime Year { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public int Seats { get; set; }

    [ForeignKey(nameof(CompanyModel))]
    public Guid CompanyId { get; set; }
    public CompanyModel? Company { get; set; }

    public DriverModel? Driver { get; set; }

    public List<BookingModel>? Bookings { get; set; }

}