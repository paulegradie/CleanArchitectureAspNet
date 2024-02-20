using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework;
using Persistence.Tables;

namespace Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUserRecord>
{
    private readonly IEntityPropertyConvention[] conventions;

    public AppDbContext(IEntityPropertyConvention[] conventions, DbContextOptions options) : base(options)
    {
        this.conventions = conventions;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var entities = typeof(AppDbContext)
            .Assembly
            .GetTypes()
            .Where(x => x.IsClass && x.GetCustomAttribute<DatabaseModelAttribute>() != null);

        foreach (var entity in entities)
        {
            var entityBuilder = modelBuilder.Entity(entity).ToTable(entity.Name);
            foreach (var entityProperty in entity.GetProperties()
                         .Where(x => x.GetCustomAttribute<NotMappedAttribute>() is null))
            {
                foreach (var convention in conventions)
                {
                    convention.Apply(modelBuilder, entityBuilder, entityProperty);
                }
            }
        }

        modelBuilder.Entity<UserOrganizationRecord>()
            .HasKey(uo => new { uo.ApplicationUserId, uo.OrganizationId });

        modelBuilder.Entity<UserOrganizationRecord>()
            .HasOne(uo => uo.ApplicationUserRecord)
            .WithMany(u => u.UserOrganizations)
            .HasForeignKey(uo => uo.ApplicationUserId);

        modelBuilder.Entity<UserOrganizationRecord>()
            .HasOne(uo => uo.OrganizationRecord)
            .WithMany(uo => uo.UserOrganizations)
            .HasForeignKey(uo => uo.OrganizationId);
    }
}