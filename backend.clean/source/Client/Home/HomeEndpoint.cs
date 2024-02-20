using Client.Contracts.Home;

namespace Client.Home;

public class HomeEndpoint : EndpointBase
{
    public HomeEndpoint(HttpClient client) : base(client)
    {
    }

    public async Task<HomeResponse> PingHome(HomeRequest homeRequest, CancellationToken cancellationToken)
        => await Get<HomeRequest, HomeResponse>(homeRequest, cancellationToken);
}