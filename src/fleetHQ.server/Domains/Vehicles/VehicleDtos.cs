namespace FleetHQ.Server.Domains.Vehicles;

public record VehicleDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime Year { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public int Seats { get; set; }
}
public record AddVehicleDto
{
    public string Type { get; set; } = string.Empty;
    public DateTime Year { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public int Seats { get; set; }
}

public record UpdateVehicleDto
{
    public string Type { get; set; } = string.Empty;
    public DateTime Year { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
}

public record DeleteVehiclesDto
{
    public List<Guid> VehicleIds { get; set; } = [];
}