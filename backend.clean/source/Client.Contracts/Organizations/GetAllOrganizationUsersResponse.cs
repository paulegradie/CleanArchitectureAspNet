namespace Client.Contracts.Organizations;


public record GetAllOrganizationUsersResponse(IEnumerable<OrganizationDto> Organizations);