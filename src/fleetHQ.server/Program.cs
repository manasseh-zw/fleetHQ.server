using FleetHQ.Server.Configuration;
using FleetHQ.Server.Extensions;
using FleetHQ.Server.Helpers;
using FleetHQ.Server.Middleware;

using Microsoft.AspNetCore.Mvc;

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


    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    builder.Services.AddControllers();

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = (actionContext) =>
        {
            var errors = actionContext.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var errorResult = XResult.Fail(errors);
            return new BadRequestObjectResult(errorResult);
        };
    });

    builder.Services.AddCors();

}


var app = builder.Build();
{

    app.MapControllers().RequireAuthorization();

    app.UseCors(p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

    app.UseHttpsRedirection();

    app.UseExceptionHandler(options => { });
}


app.Run();
