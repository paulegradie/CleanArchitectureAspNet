using Api.AccessPolicies;
using Api.Controllers;
using Api.Domain.Models;
using Api.Errors;
using Client.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Users.Auth;

public class AuthenticationController : BaseController
{
    private readonly IJwt jwt;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;

    public AuthenticationController(
        IJwt jwt,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        this.jwt = jwt;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost(SignInRequest.ActionRoute)]
    public async Task<SignInResponse> SignIn(SignInRequest signInRequest, CancellationToken cancellationToken)
    {
        var result = await signInManager
            .PasswordSignInAsync(
                signInRequest.UserName,
                signInRequest.Password,
                true,
                false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(signInRequest.UserName);
            if (user?.UserName is null) throw new NotFoundError("Could not find user after login - this should not happen!");

            var token = jwt.GenerateJwtToken(user);
            return new SignInResponse(user.UserName, token);
        }

        if (result.IsLockedOut)
        {
            throw new ForbiddenError("User account locked out");
        }

        if (result.IsNotAllowed)
        {
            throw new ForbiddenError("Not allowed to sign in");
        }

        if (result.RequiresTwoFactor)
        {
            // Handle 2FA requirement here, if applicable
            throw new NotImplementedException();
        }

        throw new ForbiddenError("Invalid login attempt");
    }

    [Authorize]
    [HttpPost("api/user/auth/sign-out")]
    public async Task SignOut(SignOutCommand signOutCommand, CancellationToken cancellationToken)
    {
        // TODO: if we can't sign out, we can't roll the security stamp
        await signInManager.SignOutAsync();
    }
}