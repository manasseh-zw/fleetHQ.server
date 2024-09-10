namespace FleetHQ.Server.Domains.Booking;
public record AddBookingDto
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerContact { get; set; } = string.Empty;
    public string CustomerLocation { get; set; } = string.Empty;
    public string CustomerDestination { get; set; } = string.Empty;
    public int PassengerCount { get; set; }
    public DateTime Time { get; set; }
    public Guid? DriverId { get; set; }
    public Guid? VehicleId { get; set; }
}

public record UpdateBookingDto
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerContact { get; set; } = string.Empty;
    public string CustomerLocation { get; set; } = string.Empty;
    public string CustomerDestination { get; set; } = string.Empty;
    public int PassengerCount { get; set; }
    public DateTime Time { get; set; }
    public Guid? DriverId { get; set; }
    public Guid? VehicleId { get; set; }
}

public record BookingDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerContact { get; set; } = string.Empty;
    public string CustomerLocation { get; set; } = string.Empty;
    public string CustomerDestination { get; set; } = string.Empty;
    public int PassengerCount { get; set; }
    public DateTime Time { get; set; }
    public Guid? DriverId { get; set; }
    public Guid? VehicleId { get; set; }
}

public record AssignDriverAndVehicleDto
{
    public Guid DriverId { get; set; }
    public Guid VehicleId { get; set; }
}