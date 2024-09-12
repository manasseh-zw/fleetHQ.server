namespace FleetHQ.Server.Domains.Driver;


public record AddDriverDto
{
    public string FullName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}

public record UpdateDriverDto
{
    public string FullName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}

public record DriverDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public Guid? VehicleId { get; set; }
    public DateTime CreatedOn { get; set; }
}

