using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFramework;

public interface IEntityPropertyConvention
{
    void Apply(ModelBuilder modelBuilder, EntityTypeBuilder entityTypeBuilder, PropertyInfo propertyInfo);
}