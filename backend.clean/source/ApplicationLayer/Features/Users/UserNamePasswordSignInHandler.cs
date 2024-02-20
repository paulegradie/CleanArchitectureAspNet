using ApplicationLayer.Contracts.Requests;
using ApplicationLayer.Contracts.Responses;
using Authentication.Abstractions;
using MediatR;

namespace ApplicationLayer.Features.Users;

public class UserNamePasswordAppSignInRequestHandler : IRequestHandler<UserNamePasswordAppSignInRequest,
    UserNamePasswordAppSignInResponse>
{
    private readonly IAuthenticator authenticator;

    public UserNamePasswordAppSignInRequestHandler(IAuthenticator authenticator)
    {
        this.authenticator = authenticator;
    }

    public async Task<UserNamePasswordAppSignInResponse> Handle(
        UserNamePasswordAppSignInRequest userNamePasswordAppSignInRequest, CancellationToken cancellationToken)
    {
        var result = await authenticator.SignIn(userNamePasswordAppSignInRequest.UserName,
            userNamePasswordAppSignInRequest.Password, cancellationToken);
        return new UserNamePasswordAppSignInResponse(result.Name, result.AuthToken);
    }
}