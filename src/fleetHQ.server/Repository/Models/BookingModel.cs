using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetHQ.Server.Repository.Models;

public class BookingModel
{
    [Key]
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerContact { get; set; } = string.Empty;
    public string CustomerLocation { get; set; } = string.Empty;
    public string CustomerDestination { get; set; } = string.Empty;
    public int PassengerCount { get; set; }
    public DateTime Time { get; set; }

    [ForeignKey(nameof(DriverModel))]
    public Guid? DriverId { get; set; }
    public DriverModel? Driver { get; set; }

    [ForeignKey(nameof(VehicleModel))]
    public Guid? VehicleId { get; set; }
    public VehicleModel? Vehicle { get; set; }


}