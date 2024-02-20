using Client.Contracts.User;
using IntegrationTests.Base;
using IntegrationTests.Utils;
using Shouldly;

namespace IntegrationTests.Users;

public class UsersTests : IntegrationTest
{
    [Fact]
    public async Task RegistrationSucceeds()
    {
        var userName = Some.RandomUserName();
        var response = await Client.Users.Register(new RegisterRequest(userName, "Qwerty_123"), CancellationToken);
        response.UserName.ShouldBe(userName);
    }
}