using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Abstractions;
using Persistence.EntityFramework;
using Persistence.Services;

namespace Composition.Autofac;

public class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        base.Load(builder);
        builder.Register(c =>
        {
            var conventions = c.Resolve<IEntityPropertyConvention[]>();
            var config = c.Resolve<IConfiguration>();
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseSqlServer(config.GetConnectionString("ConnectionString"));
            return new AppDbContext(conventions, options.Options);
        });
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        builder.RegisterType<UserRetriever>().As<IUserRetriever>().InstancePerLifetimeScope();

        builder
            .RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IMapToDatabaseRecords<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}