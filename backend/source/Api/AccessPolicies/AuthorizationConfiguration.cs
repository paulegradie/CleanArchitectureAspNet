using System.Security.Claims;

namespace Api.AccessPolicies;

public static class AuthorizationConfiguration
{
    public static void ConfigureRolePolicies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorization(opts => { opts.AddPolicy(Policies.AdminPolicy, policy => { policy.RequireClaim(ClaimTypes.Role, UserRoles.AdminRole); }); });
    }
}