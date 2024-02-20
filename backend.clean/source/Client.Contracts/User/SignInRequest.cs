namespace Client.Contracts.User;

public record SignInRequest(string UserName, string Password) : RequestBase
{
    public const string ActionRoute = "api/user/auth/sign-in";
    public override string GetActionRoute() => ActionRoute;
}