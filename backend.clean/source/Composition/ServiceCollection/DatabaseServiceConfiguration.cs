using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Tables;

namespace Composition.ServiceCollection;

public static class DatabaseServiceConfiguration
{
    public static void ConfigureDatabaseServices(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        serviceCollection
            .AddDbContext<AppDbContext>(
                opts => { opts.UseSqlServer(configuration.GetConnectionString("ConnectionString")); });

        serviceCollection
            .AddIdentity<ApplicationUserRecord, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}