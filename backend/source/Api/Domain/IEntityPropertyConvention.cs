using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Domain;

public interface IEntityPropertyConvention
{
    void Apply(ModelBuilder modelBuilder, EntityTypeBuilder entityTypeBuilder, PropertyInfo propertyInfo);
}