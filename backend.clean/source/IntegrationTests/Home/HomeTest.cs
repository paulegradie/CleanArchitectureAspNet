using Client.Contracts.Home;
using IntegrationTests.Base;
using Shouldly;

namespace IntegrationTests.Home;

public class HomeTest : IntegrationTest
{
 
    [Fact]
    public async Task PingHome()
    {
        var response = await Client.Home.PingHome(new HomeRequest(), CancellationToken);
        response.Hello.ShouldBe("Hello from the API!");
    }   
}