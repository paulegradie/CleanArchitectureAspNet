using Persistence.Tables;

namespace Persistence.Services;

public interface IUserRetriever
{
    Task<ApplicationUserRecord> GetAdminUser();
}