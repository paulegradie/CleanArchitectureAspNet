using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Services;
using Persistence.Tables;

namespace Persistence.Repositories;

public class OrganizationsRepository : BaseRepository<OrganizationRecord>, IOrganizationsRepository
{
    private readonly IMapToTheDomain<OrganizationRecord, Organization> toOrganizationDomainMapper;
    private readonly IUserRetriever userRetriever;

    public OrganizationsRepository(
        IMapToTheDomain<OrganizationRecord, Organization> toOrganizationDomainMapper,
        IUserRetriever userRetriever,
        AppDbContext context) : base(context)
    {
        this.toOrganizationDomainMapper = toOrganizationDomainMapper;
        this.userRetriever = userRetriever;
    }

    public async Task<Organization> AddOrganization(string name, CancellationToken cancellationToken)
    {
        var adminUser = await userRetriever.GetAdminUser();

        var newOrganization = new OrganizationRecord(name);
        var userOrganization = new UserOrganizationRecord(adminUser, newOrganization);

        adminUser.UserOrganizations.Add(userOrganization);
        newOrganization.AddUserOrganization(userOrganization);

        var newOrg = context.Set<OrganizationRecord>().Add(newOrganization);
        return await toOrganizationDomainMapper.Map(newOrg.Entity, cancellationToken);
    }

    public async Task<IEnumerable<Organization>> GetAllOrganizations(CancellationToken cancellationToken)
    {
        // admin because only an admin an access this controller!
        var adminUser = await userRetriever.GetAdminUser();

        var organizations = await context.Set<UserOrganizationRecord>()
            .Where(uo => uo.ApplicationUserId == adminUser.Id)
            .Include(uo => uo.OrganizationRecord)
            .ThenInclude(org => org.UserOrganizations)
            .ThenInclude(uo => uo.ApplicationUserRecord)
            .Select(uo => uo.OrganizationRecord)
            .Distinct()
            .ToListAsync(cancellationToken);

        var orgs = new List<Organization>();
        foreach (var org in organizations)
        {
            var domain = await toOrganizationDomainMapper.Map(org, cancellationToken);
            orgs.Add(domain);
        }

        return orgs;

        // return organizations.Select(org => org.ToDomain());


        // var organizationResponses = new List<OrganizationGroup>();
        // foreach (var organization in organizations)
        // {
        //     var usersInCurrentOrganization = organization
        //         .UserOrganizations
        //         .Select(x => new OrganizationUser(x.ApplicationUserRecord.UserName!));
        //     
        //     
        //     
        //     organizationResponses.Add(new OrganizationGroup(organization.Name, usersInCurrentOrganization));
        // }
        //
        // return new GetAllOrganizationUsersResponse(organizationResponses);
    }
}