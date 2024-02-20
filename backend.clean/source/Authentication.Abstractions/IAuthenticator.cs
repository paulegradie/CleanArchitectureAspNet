namespace Authentication.Abstractions;

public interface IAuthenticator
{
    Task<AppSignInResult> SignIn(string userName, string password, CancellationToken cancellationToken);
    Task SignOut(CancellationToken cancellationToken);
    Task<RegistrationResult> Register(string userName, string password, CancellationToken cancellationToken);
}

