using Api.AccessPolicies;
using Api.Controllers;
using Api.Domain.Models;
using Api.Errors;
using Client.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Users.Admin;

public class AdminUserController : AdminOnlyBaseController
{
    private readonly UserManager<ApplicationUser> userManager;

    public AdminUserController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost(RegisterRequest.ActionRoute)]
    public async Task<RegisterResponse> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var userName = registerRequest.UserName;
        var password = registerRequest.Password;

        var newAdminUser = new ApplicationUser(true, userName);
        var newUserResult = await userManager.CreateAsync(newAdminUser, password);
        if (newUserResult is null || !newUserResult.Succeeded || newAdminUser is null)
        {
            var msg = newUserResult?.Errors.Select(x => x.Description);
            throw new BadRequestError(msg is null ? "Failed to create new user" : string.Join(", ", msg));
        }

        await userManager.AddToRoleAsync(newAdminUser, UserRoles.AdminRole);
        return new(newAdminUser.UserName!);
    }
}