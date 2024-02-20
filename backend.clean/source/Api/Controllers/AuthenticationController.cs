using Api.Controllers.Bases;
using ApplicationLayer.Contracts.Requests;
using Client.Contracts.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IMediator mediator;

    public AuthenticationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost(SignInRequest.ActionRoute)]
    public async Task<SignInResponse> SignIn(SignInRequest signInRequest, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(
            new UserNamePasswordAppSignInRequest(signInRequest.UserName, signInRequest.Password),
            cancellationToken);

        return new SignInResponse(response.UserName, response.AuthToken);
    }

    [Authorize]
    [HttpPost("api/user/auth/sign-out")]
    public async Task SignOut(SignOutCommand _, CancellationToken cancellationToken)
    {
        await mediator.Send(new AppSignOutRequest(), cancellationToken);
    }
}