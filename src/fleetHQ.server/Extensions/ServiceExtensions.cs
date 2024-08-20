using System.Text;

using FleetHQ.Server.Authorization;
using FleetHQ.Server.Configuration;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FleetHQ.Server.Extensions;

public static class ServiceExtensions
{

    public static void ConfigurePostgres(this IServiceCollection services)
    {
        services.AddDbContext<RepositoryContext>(
            options => options.UseNpgsql(Appsettings.DatabaseOptions.Postgres));
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
            foreach (var feature in AppFeatures.All)
            {
                options.AddPolicy($"{feature}View", policy =>
                    policy.Requirements.Add(new AccessControlRequirement(feature, Access.View)));
                options.AddPolicy($"{feature}Edit", policy =>
                    policy.Requirements.Add(new AccessControlRequirement(feature, Access.Edit)));
            }
        });

        services.AddScoped<IAuthorizationHandler, AccessControlHandler>();
    }
}