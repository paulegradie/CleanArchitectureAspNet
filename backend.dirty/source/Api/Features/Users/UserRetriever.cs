using System.Security.Claims;
using Api.Domain.Models;
using Api.Errors;
using Microsoft.AspNetCore.Identity;

namespace Api.Features.Users;

internal interface IUserRetriever
{
    Task<ApplicationUser> GetAdminUser();
}

internal class UserRetriever : IUserRetriever
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IHttpContextAccessor contextAccessor;

    public UserRetriever(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
    {
        this.userManager = userManager;
        this.contextAccessor = contextAccessor;
    }

    public async Task<ApplicationUser> GetAdminUser()
    {
        var userPrincipal = contextAccessor.HttpContext.User;
        var authenticatedUser = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NotFoundError("Could not find admin user");
        var adminUser = await userManager.FindByNameAsync(authenticatedUser);

        if (adminUser == null)
        {
            throw new NotFoundError("Admin user not found");
        }

        return adminUser;
    }
}