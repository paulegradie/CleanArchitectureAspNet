using ApplicationLayer.Contracts.Responses;
using MediatR;

namespace ApplicationLayer.Contracts.Requests;

public record UserNamePasswordAppSignInRequest(string UserName, string Password) : IRequest<UserNamePasswordAppSignInResponse>;