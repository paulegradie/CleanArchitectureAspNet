using Api.Domain;

namespace Api.Database;

public static class DatabaseMigrator
{
    public static void EnsureAndMigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // if (app.Environment.IsDevelopment())
        // {
        // }
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        // dbContext.Database.Migrate();
    }
}