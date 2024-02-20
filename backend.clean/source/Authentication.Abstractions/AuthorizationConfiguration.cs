using System.Security.Claims;
using Authentication.Abstractions.AccessPolicies;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Abstractions;

public static class AuthorizationConfiguration
{
    public static void ConfigureRolePolicies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorization(opts => { opts.AddPolicy(UserPolicies.AdminPolicy, policy => { policy.RequireClaim(ClaimTypes.Role, UserRoles.AdminRole); }); });
    }
}