using Client.Home;
using Client.User;
using IntegrationTests.Base;
using Shouldly;

namespace IntegrationTests.Users;

public class UsersTests : IntegrationTest
{
    [Fact]
    public async Task RegistrationSucceeds()
    {
        const string userName = "TestUser";

        var response = await Client.Users.Register(new RegisterRequest(userName, "Qwerty_123"), CancellationToken);
        response.UserName.ShouldBe(userName);
    }

    [Fact]
    public async Task PingHome()
    {
        var response = await Client.Home.PingHome(new HomeRequest(), CancellationToken);
        response.Hello.ShouldBe("Hello from the API!");
    }
}

public class HomeTest : IntegrationTest
{
    
}