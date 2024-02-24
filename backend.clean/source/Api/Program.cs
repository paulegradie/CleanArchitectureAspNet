using Api.Middleware;
using Authentication.Abstractions;
using Autofac.Extensions.DependencyInjection;
using Composition;
using Composition.Persistence;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    // TODO: Get assemblies more precisely.
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();

    builder.Services.RegisterServiceProviders();
    builder.Host.ConfigureRegistrations();

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();

    var app = builder.Build();

    app.Services.EnsureAndMigrateDatabase();
    await app.EnsureDefaultRoles();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseMiddleware<UnitOfWorkMiddleware>();
    app.Run();

    return 0;
}
catch (Exception ex)
{
    Console.WriteLine("Exception encountered on startup");
    Console.WriteLine(ex.Message);
    if (ex.InnerException is not null)
    {
        Console.WriteLine(ex.InnerException);
    }

    return 1;
}


// https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0#aspnet-core-integration-tests

namespace Api
{
    public partial class Program
    {
    }
}