using ApplicationLayer.Contracts.Responses;
using MediatR;

namespace ApplicationLayer.Contracts.Requests;

public record CreateAppOrganizationRequest(string Name) : IRequest<CreateAppOrganizationResponse>;