namespace FleetHQ.Server.Configuration;

public class Appsettings
{
    public const string ConnectionStrings = "ConnectionStrings";
    public const string JwtConfig = "JwtConfig";
    public static DatabaseOptions DatabaseOptions { get; set; } = new();
    public static JwtOptions JwtOptions { get; set; } = new();

}

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
}

public class DatabaseOptions
{
    public string Postgres { get; set; } = string.Empty;
}