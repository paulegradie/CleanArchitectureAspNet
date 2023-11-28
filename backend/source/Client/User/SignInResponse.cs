namespace Client.User;

public class SignInResponse
{
    public SignInResponse(string userName, string jwtToken)
    {
        UserName = userName;
        JwtToken = jwtToken;
    }

    public string UserName { get; private set; }
    public string JwtToken { get; private set; }
}