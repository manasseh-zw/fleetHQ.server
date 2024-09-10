namespace FleetHQ.Server.Domains.Driver;


public record AddDriverDto
{
    public string FullName { get; set; }
    public string ContactNumber { get; set; }
    public string Address { get; set; }
    public DateTime HireDate { get; set; }
}

public record UpdateDriverDto
{
    public string FullName { get; set; }
    public string ContactNumber { get; set; }
    public string Address { get; set; }
    public DateTime HireDate { get; set; }
}

public record DriverDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string ContactNumber { get; set; }
    public string Address { get; set; }
    public DateTime HireDate { get; set; }
    public Guid? VehicleId { get; set; }
    public DateTime CreatedOn { get; set; }
}

