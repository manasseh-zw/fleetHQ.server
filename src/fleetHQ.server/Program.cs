using FleetHQ.Server.Configuration;
using FleetHQ.Server.Exceptions;
using FleetHQ.Server.Extensions;

using Scrutor;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.GetSection(Appsettings.ConnectionStrings)
    .Bind(Appsettings.DatabaseOptions);

    builder.Configuration.GetSection(Appsettings.JwtConfig)
    .Bind(Appsettings.JwtOptions);

    builder.Services.ConfigurePostgres();
    builder.Services.ConfigureAuthentication();
    builder.Services.ConfigureAuthorization();

    builder.Services.Scan(x =>
        x.FromAssemblies(typeof(Program).Assembly)
        .AddClasses()
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .WithScopedLifetime()
    );

    builder.Services.AddExceptionHandler<ExceptionHandler>();

    builder.Services.AddControllers();

    builder.Services.AddCors();

}


var app = builder.Build();
{
    app.UseHttpsRedirection();
    
    app.UseAuthorization();

    app.MapControllers().RequireAuthorization();

    app.UseCors(p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

    app.UseExceptionHandler(options => { });
}


app.Run();
