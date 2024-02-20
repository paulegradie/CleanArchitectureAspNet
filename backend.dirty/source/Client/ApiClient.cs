using Client.Home;
using Client.Organizations;
using Client.User;

namespace Client;

public class ApiClient
{
    public ApiClient(HttpClient client)
    {
        Users = new UserEndpoint(client);
        Organizations = new OrganizationEndpoint(client);
        Home = new HomeEndpoint(client);
        Http = client;
    }

    public HttpClient Http { get; set; }
    public HomeEndpoint Home { get; set; }
    public UserEndpoint Users { get; set; }
    public OrganizationEndpoint Organizations { get; set; }
}