using ApplicationLayer.Contracts.Responses;
using MediatR;

namespace ApplicationLayer.Contracts.Requests;

public record AppSignOutRequest() : IRequest<AppSignOutResponse>;