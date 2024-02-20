using MediatR;

namespace ApplicationLayer.Contracts.Requests;

public record GetAllAppOrganizationsRequest() : IRequest<GetAllAppOrganizationsResponse>;