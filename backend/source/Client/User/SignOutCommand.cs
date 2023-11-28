namespace Client.User;

public class SignOutCommand
{
    public SignOutCommand(string userName, string jwtToken)
    {
        UserName = userName;
        JwtToken = jwtToken;
    }

    public string UserName { get; init; }
    public string JwtToken { get; init; }
}