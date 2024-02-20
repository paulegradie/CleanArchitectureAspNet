using Client.Home;
using Client.Organizations;
using Client.User;

namespace Client;

public class ServerClient(HttpClient client)
{
    public HttpClient Http { get; set; } = client;
    public HomeEndpoint Home { get; set; } = new(client);
    public UserEndpoint Users { get; set; } = new(client);
    public OrganizationEndpoint Organizations { get; set; } = new(client);
}