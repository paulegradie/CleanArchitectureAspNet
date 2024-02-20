using Api.Controllers.Bases;
using ApplicationLayer.Contracts.Requests;
using Client.Contracts.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AdminUserController : AdminOnlyBaseController
{
    private readonly IMediator mediator;

    public AdminUserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost(RegisterRequest.ActionRoute)]
    public async Task<RegisterResponse> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new AppNewUserRegistrationRequest(registerRequest.UserName, registerRequest.Password), cancellationToken);
        return new RegisterResponse(response.UserName);
    }
}