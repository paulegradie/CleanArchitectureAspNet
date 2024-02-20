using Domain.Abstractions;
using Domain.Models;
using Domain.Repositories;
using Persistence.Services;
using Persistence.Tables;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IUserRetriever userRetriever;
    private readonly IMapToTheDomain<ApplicationUserRecord, User> domainMapper;

    public UserRepository(IUserRetriever userRetriever, IMapToTheDomain<ApplicationUserRecord, User> domainMapper)
    {
        this.userRetriever = userRetriever;
        this.domainMapper = domainMapper;
    }

    public async Task<User> GetAdminUser(CancellationToken cancellationToken)
    {
        var userRecord = await userRetriever.GetAdminUser();
        return await domainMapper.Map(userRecord, cancellationToken);
    }
}