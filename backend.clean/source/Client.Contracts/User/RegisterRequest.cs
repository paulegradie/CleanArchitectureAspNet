namespace Client.Contracts.User;

public record RegisterRequest(string UserName, string Password) : RequestBase
{
    public const string ActionRoute = "api/user/register";
    public override string GetActionRoute() => ActionRoute;
}