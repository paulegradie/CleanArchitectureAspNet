namespace ApplicationLayer.Contracts.Responses;

public record UserNamePasswordAppSignInResponse(string UserName, string AuthToken);