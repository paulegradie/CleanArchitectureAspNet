using System.Security.Authentication;
using Authentication.Abstractions;
using Authentication.Abstractions.AccessPolicies;
using Microsoft.AspNetCore.Identity;
using Persistence.Tables;

namespace Authentication;

public class Authenticator : IAuthenticator
{
    private readonly IJwt jwt;
    private readonly UserManager<ApplicationUserRecord> userManager;
    private readonly SignInManager<ApplicationUserRecord> signInManager;

    public Authenticator(
        IJwt jwt,
        UserManager<ApplicationUserRecord> userManager,
        SignInManager<ApplicationUserRecord> signInManager)
    {
        this.jwt = jwt;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<AppSignInResult> SignIn(string userName, string password, CancellationToken cancellationToken)
    {
        var result = await signInManager
            .PasswordSignInAsync(
                userName,
                password,
                true,
                false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user?.UserName is null)
                throw new AuthenticationException("Could not find user after login - this should not happen!");

            var token = jwt.GenerateJwtToken(user.IsAdmin, user.UserName);
            return new AppSignInResult(user.UserName, token);
        }

        if (result.IsLockedOut)
        {
            throw new AuthenticationException("User account locked out");
        }

        if (result.IsNotAllowed)
        {
            throw new AuthenticationException("Not allowed to sign in");
        }

        if (result.RequiresTwoFactor)
        {
            // Handle 2FA requirement here, if applicable
            throw new NotImplementedException("This should be implemented later");
        }

        throw new AuthenticationException("Invalid login attempt");
    }

    public async Task SignOut(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) return;
        await signInManager.SignOutAsync();
    }

    public async Task<RegistrationResult> Register(
        string userName,
        string password,
        CancellationToken cancellationToken)
    {
        var newAdminUser = new ApplicationUserRecord(true, userName);
        var newUserResult = await userManager.CreateAsync(newAdminUser, password);
        if (newUserResult is null || !newUserResult.Succeeded || newAdminUser is null)
        {
            var msg = newUserResult?.Errors.Select(x => x.Description);
            throw new AuthenticationException(msg is null ? "Failed to create new user" : string.Join(", ", msg));
        }

        await userManager.AddToRoleAsync(newAdminUser, UserRoles.AdminRole);
        return new RegistrationResult(newAdminUser.UserName!);
    }
}