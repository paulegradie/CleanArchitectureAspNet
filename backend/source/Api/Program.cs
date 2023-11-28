using Api;
using Api.AccessPolicies;
using Api.Database;
using Api.Domain;
using Api.Domain.Models;
using Api.Middleware;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .CreateLogger();
Log.Logger.Information("Starting AspNetApiStarter Server");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.ConfigureDatabaseServices(builder.Configuration);
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSwaggerGen();
    builder.Services
        .AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    builder.Services.ConfigureAuthentication(builder.Configuration);
    builder.Services.ConfigureRolePolicies();
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.UseSerilog();

    builder.Environment.EnvironmentName = builder.Configuration.Environment();

    builder.Services.AddMediatR(configuration => { configuration.RegisterServicesFromAssembly(typeof(Program).Assembly); });
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddFluentValidation(new[] { typeof(Program).Assembly });
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    builder.Services.AddSingleton(Log.Logger);
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly);
        containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces();
    });

    var app = builder.Build();
    app.EnsureAndMigrateDatabase();
    await app.EnsureDefaultRoles();

    app.UseSerilogRequestLogging();
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
finally
{
    Log.CloseAndFlush();
}

// https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0#aspnet-core-integration-tests

public partial class Program
{
}