using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Api.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain;

public class AppDbContext : IdentityDbContext<ApplicationUser>
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
            .Where(x => x.IsClass && x.GetCustomAttribute<EntityAttribute>() != null);

        foreach (var entity in entities)
        {
            var entityBuilder = modelBuilder.Entity(entity).ToTable(entity.Name);
            foreach (var entityProperty in entity.GetProperties().Where(x => x.GetCustomAttribute<NotMappedAttribute>() is null))
            {
                foreach (var convention in conventions)
                {
                    convention.Apply(modelBuilder, entityBuilder, entityProperty);
                }
            }
        }

        modelBuilder.Entity<UserOrganization>()
            .HasKey(uo => new { uo.ApplicationUserId, uo.OrganizationId });

        modelBuilder.Entity<UserOrganization>()
            .HasOne(uo => uo.ApplicationUser)
            .WithMany(u => u.UserOrganizations)
            .HasForeignKey(uo => uo.ApplicationUserId);

        modelBuilder.Entity<UserOrganization>()
            .HasOne(uo => uo.Organization)
            .WithMany(uo => uo.UserOrganizations)
            .HasForeignKey(uo => uo.OrganizationId);
    }
}