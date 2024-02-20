using ApplicationLayer.Contracts.Responses;
using MediatR;

namespace ApplicationLayer.Contracts.Requests;

public record AppNewUserRegistrationRequest(string UserName, string Password) : IRequest<AppNewUserRegistrationResponse>;