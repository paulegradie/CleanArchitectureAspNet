using ApplicationLayer.Contracts.Requests;
using ApplicationLayer.Contracts.Responses;
using Authentication.Abstractions;
using MediatR;

namespace ApplicationLayer.Features.Users;

public class AppSignOutRequestHandler : IRequestHandler<AppSignOutRequest, AppSignOutResponse>
{
    private readonly IAuthenticator authenticator;

    public AppSignOutRequestHandler(IAuthenticator authenticator)
    {
        this.authenticator = authenticator;
    }

    public async Task<AppSignOutResponse> Handle(AppSignOutRequest request, CancellationToken cancellationToken)
    {
        await authenticator.SignOut(cancellationToken);
        return new AppSignOutResponse();
    }
}