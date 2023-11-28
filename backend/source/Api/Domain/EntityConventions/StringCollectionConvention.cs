using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Domain.EntityConventions;

public class StringCollectionConvention : IEntityPropertyConvention
{
    private const string Separator = "<sep>";

    public void Apply(ModelBuilder modelBuilder, EntityTypeBuilder entityTypeBuilder, PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType != typeof(List<string>)) return;

        entityTypeBuilder.Property(propertyInfo.Name).HasConversion(
            new ValueConverter<List<string>, string>(
                list => string.Join(Separator, list),
                s => s.Split(Separator, StringSplitOptions.TrimEntries).ToList()));
    }
}