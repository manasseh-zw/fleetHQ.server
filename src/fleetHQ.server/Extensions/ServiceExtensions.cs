using System.Text;

using FleetHQ.Server.Authorization;
using FleetHQ.Server.Configuration;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Npgsql;

namespace FleetHQ.Server.Extensions;

public static class ServiceExtensions
{

    public static void ConfigurePostgres(this IServiceCollection services)
    {
        services.AddDbContext<RepositoryContext>(
            options =>
            {
                var db = new NpgsqlDataSourceBuilder(Appsettings.DatabaseOptions.Postgres);
                db.EnableDynamicJson();
                options.UseNpgsql(db.Build());
            });
    }


    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Bearer", options => options.TokenValidationParameters = new TokenValidationParameters
        {
            SaveSigninToken = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            ValidIssuer = Appsettings.JwtOptions.Issuer,
            ValidAudience = Appsettings.JwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Appsettings.JwtOptions.Secret)
                    )
        });

    }

    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
       {
           foreach (var feature in Features.All)
           {
               options.AddPolicy($"{feature}.Read", policy =>
                   policy.Requirements.Add(new PermissionRequirement(feature, Permission.Read)));
               options.AddPolicy($"{feature}.Write", policy =>
                   policy.Requirements.Add(new PermissionRequirement(feature, Permission.Write)));
           }
       });

        services.AddScoped<IAuthorizationHandler, PermissionsAuthorizationHandler>();
    }
}