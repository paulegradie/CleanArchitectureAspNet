using ApplicationLayer.Contracts.Requests;
using ApplicationLayer.Contracts.Responses;
using Authentication.Abstractions;
using MediatR;

namespace ApplicationLayer.Features.Users;

public class NewUserRegistrationRequestHandler
    : IRequestHandler<AppNewUserRegistrationRequest, AppNewUserRegistrationResponse>
{
    private readonly IAuthenticator authenticator;

    public NewUserRegistrationRequestHandler(IAuthenticator authenticator)
    {
        this.authenticator = authenticator;
    }

    public async Task<AppNewUserRegistrationResponse> Handle(
        AppNewUserRegistrationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await authenticator.Register(request.UserName, request.Password, cancellationToken);
        return await Task.FromResult(new AppNewUserRegistrationResponse(result.UserName));
    }
}