using Client.Contracts.User;

namespace Client.User;

public class UserEndpoint(HttpClient client) : EndpointBase(client)
{
    public async Task<RegisterResponse> Register(RegisterRequest signInRequest, CancellationToken cancellationToken)
        => await Post<RegisterRequest, RegisterResponse>(signInRequest, cancellationToken);

    public async Task<SignInResponse> SignIn(SignInRequest signInRequest, CancellationToken cancellationToken)
        => await Post<SignInRequest, SignInResponse>(signInRequest, cancellationToken);
}