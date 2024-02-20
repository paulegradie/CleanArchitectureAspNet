using System.Security.Claims;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence.Exceptions;
using Persistence.Tables;

namespace Persistence.Services;

internal class UserRetriever : IUserRetriever
{
    private readonly IMapToTheDomain<ApplicationUserRecord, User> toDomainUserMapper;
    private readonly UserManager<ApplicationUserRecord> userManager;
    private readonly IHttpContextAccessor contextAccessor;

    public UserRetriever(
        IMapToTheDomain<ApplicationUserRecord, User> toDomainUserMapper,
        UserManager<ApplicationUserRecord> userManager,
        IHttpContextAccessor contextAccessor)
    {
        this.toDomainUserMapper = toDomainUserMapper;
        this.userManager = userManager;
        this.contextAccessor = contextAccessor;
    }

    public async Task<ApplicationUserRecord> GetAdminUser()
    {
        var userPrincipal = contextAccessor?.HttpContext?.User ??
                            throw new UserNotFoundException("No user found associated with this request");
        var authenticatedUser = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                throw new UserNotFoundException("Could not find admin user");
        var adminUser = await userManager.FindByNameAsync(authenticatedUser);

        if (adminUser == null)
        {
            throw new UserNotFoundException("Admin user not found");
        }

        return adminUser;
    }
}