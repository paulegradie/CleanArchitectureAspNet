using Api.Domain;
using Api.Domain.Models;
using Api.Features.Users;
using Client.Organizations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization = Client.Organizations.Organization;

namespace Api.Features.Organizations;

internal class GetAllOrganizationsAndUsersHandler : IRequestHandler<GetAllOrganizationsRequest, GetAllOrganizationUsersResponse>
{
    private readonly IUserRetriever userRetriever;
    private readonly AppDbContext dbContext;

    public GetAllOrganizationsAndUsersHandler(
        IUserRetriever userRetriever,
        AppDbContext dbContext)
    {
        this.userRetriever = userRetriever;
        this.dbContext = dbContext;
    }

    public async Task<GetAllOrganizationUsersResponse> Handle(GetAllOrganizationsRequest request, CancellationToken cancellationToken)
    {
        // admin because only an admin an access this controller!
        var adminUser = await userRetriever.GetAdminUser();
        var organizations = await dbContext.Set<UserOrganization>()
            .Where(uo => uo.ApplicationUserId == adminUser.Id)
            .Include(uo => uo.Organization)
            .ThenInclude(org => org.UserOrganizations)
            .ThenInclude(uo => uo.ApplicationUser)
            .Select(uo => uo.Organization)
            .Distinct()
            .ToListAsync(cancellationToken);

        var organizationResponses = new List<Organization>();
        foreach (var organization in organizations)
        {
            var usersInCurrentOrganization = organization
                .UserOrganizations
                .Select(x => new OrganizationUser(x.ApplicationUser.UserName!));
            organizationResponses.Add(new Organization(organization.Name, usersInCurrentOrganization));
        }

        return new GetAllOrganizationUsersResponse(organizationResponses);
    }
}