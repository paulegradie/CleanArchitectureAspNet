using Api.Middleware;
using Api.Registrations;
using ApplicationLayer.Registrations;
using Authentication.Abstractions;
using Autofac.Extensions.DependencyInjection;
using Composition;
using Persistence;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSwaggerGen();

    builder.Services.ConfigureDatabaseServices(builder.Configuration);

    builder.Services.ConfigureAuthentication(builder.Configuration);
    builder.Services.ConfigureRolePolicies();
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Services.AddHttpContextAccessor();

    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    builder.Services.RegisterApplicationLayerHandlers(assemblies);

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();


    builder.Environment.EnvironmentName = "Development"; //builder.Configuration.Environment();
    builder.Host.ConfigureRegistrations();
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