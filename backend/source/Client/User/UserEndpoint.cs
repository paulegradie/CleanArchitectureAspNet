namespace Client.User;

public class UserEndpoint : EndpointBase
{
    public UserEndpoint(HttpClient client) : base(client)
    {
    }

    public async Task<RegisterResponse> Register(RegisterRequest signInRequest, CancellationToken cancellationToken)
        => await Post<RegisterRequest, RegisterResponse>(signInRequest, cancellationToken);

    public async Task<SignInResponse> SignIn(SignInRequest signInRequest, CancellationToken cancellationToken)
        => await Post<SignInRequest, SignInResponse>(signInRequest, cancellationToken);
}