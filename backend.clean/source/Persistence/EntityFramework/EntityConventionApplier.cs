namespace Persistence.EntityFramework;

public class EntityConventionApplier :IEntityConventionApplier
{
    private readonly IEntityPropertyConvention[] conventions;

    // autofac returns an array of registrations when multiple are registered under the same name
    public EntityConventionApplier(IEntityPropertyConvention[] conventions)
    {
        this.conventions = conventions;
    }
    
    public void Apply()
    {
        throw new NotImplementedException();
    }
}