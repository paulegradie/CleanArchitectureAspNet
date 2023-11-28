using Api.Domain;
using Api.Domain.Models;
using Api.Features.Users;
using Client.Organizations;
using MediatR;
using Organization = Api.Domain.Models.Organization;

namespace Api.Features.Organizations;

internal class CreateNewOrganizationHandler : IRequestHandler<CreateOrganizationRequest, CreateOrganizationResponse>
{
    private readonly IUserRetriever userRetriever;
    private readonly AppDbContext dbContext;

    public CreateNewOrganizationHandler(
        IUserRetriever userRetriever,
        AppDbContext dbContext)
    {
        this.userRetriever = userRetriever;
        this.dbContext = dbContext;
    }

    public async Task<CreateOrganizationResponse> Handle(CreateOrganizationRequest request, CancellationToken cancellationToken)
    {
        var adminUser = await userRetriever.GetAdminUser();

        var newOrganization = new Organization(request.Name);
        var userOrganization = new UserOrganization(adminUser, newOrganization);

        adminUser.UserOrganizations.Add(userOrganization);
        newOrganization.AddUserOrganization(userOrganization);

        dbContext.Set<Organization>().Add(newOrganization);
        return new CreateOrganizationResponse(newOrganization.Name);
    }
}