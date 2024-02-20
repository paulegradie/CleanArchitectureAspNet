using Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public static class DatabaseServiceConfiguration
{
    public static void ConfigureDatabaseServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddDbContext<AppDbContext>(
                opts => { opts.UseSqlServer(configuration.GetConnectionString("ConnectionString")); });
}