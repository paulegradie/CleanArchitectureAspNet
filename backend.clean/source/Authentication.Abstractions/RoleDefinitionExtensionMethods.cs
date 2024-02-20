using Authentication.Abstractions.AccessPolicies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Abstractions;

public static class RoleDefinitionExtensionMethods
{
    private static readonly string[] DefaultRoles;

    static RoleDefinitionExtensionMethods()
    {
        DefaultRoles = new[] { UserRoles.AdminRole, UserRoles.MemberRole };
    }

    public static async Task EnsureDefaultRoles(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await SetDefaultRoles(scope.ServiceProvider);
    }

    public static async Task EnsureDefaultRoles(this IServiceProvider serviceProvider)
        => await SetDefaultRoles(serviceProvider);


    private static async Task SetDefaultRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in DefaultRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}