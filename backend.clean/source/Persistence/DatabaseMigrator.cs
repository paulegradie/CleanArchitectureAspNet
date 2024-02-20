using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DatabaseMigrator
{
    public static void EnsureAndMigrateDatabase(
        this IServiceProvider provider,
        CancellationToken cancellationToken = default)
    {
        using var scope = provider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();

        // var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        // if (app.Environment.IsDevelopment())
        // {
        // }
        // dbContext.Persistence.Migrate();
    }
}