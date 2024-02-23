using System.Security.Claims;
using System.Text;
using Authentication.Abstractions.AccessPolicies;
using Authentication.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Composition.ServiceCollection;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        serviceCollection.RegisterAuthenticationOptions(configuration);
        serviceCollection.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                var serviceProvider = serviceCollection.BuildServiceProvider();
                var authSettings = serviceProvider.GetRequiredService<IOptions<AuthenticationOptions>>();

                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = configuration.GetValue<string>("Environment") == "Production",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Value.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role
                };
            });

        serviceCollection.ConfigureRolePolicies();
    }

    private static void ConfigureRolePolicies(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddAuthorizationBuilder()
            .AddPolicy(UserPolicies.AdminPolicy,
                policy => { policy.RequireClaim(ClaimTypes.Role, UserRoles.AdminRole); });
    }

    private static void RegisterAuthenticationOptions(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.Configure<AuthenticationOptions>(configuration.GetSection("Auth"));
    }
}