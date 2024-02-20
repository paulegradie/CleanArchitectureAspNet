using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace IntegrationTests.Base;

public class Server : WebApplicationFactory<Api.Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new CustomServiceProviderFactory());
        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureTestServices(services =>
            {
                var dbContextDescriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (dbContextDescriptor is not null) services.Remove(dbContextDescriptor);

                var dbConnDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
                if (dbConnDescriptor is not null) services.Remove(dbConnDescriptor);

                services.AddDbContext<AppDbContext>(opts =>
                    opts.UseSqlServer(
                        "Server=localhost;Persistence=IntegrationTesting;User Id=sa;Password=Password01!;TrustServerCertificate=True;Encrypt=False"));
            });

        base.ConfigureWebHost(builder);
    }
}