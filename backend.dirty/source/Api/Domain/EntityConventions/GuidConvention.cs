using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Domain.EntityConventions;

public class GuidConvention : IEntityPropertyConvention
{
    public void Apply(ModelBuilder modelBuilder, EntityTypeBuilder entityTypeBuilder, PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType != typeof(Guid)) return;

        entityTypeBuilder.Property(propertyInfo.Name).HasConversion(
            new ValueConverter<Guid, string>(
                guid => guid.ToString(),
                s => Guid.Parse(s)
            )
        );
    }
}