using Rnd.Api.Data;
using Rnd.Api.Helpers;

namespace Rnd.Api.Modules.Basic.Resources;

public class ResourceFactory : IStorableFactory<Data.Entities.Resource>
{
    public static IResource Create(Data.Entities.Resource entity)
    {
        var factory = new ResourceFactory();
        return (Resource) factory.CreateStorable(entity);
    }
    
    public IStorable<Data.Entities.Resource> CreateStorable(Data.Entities.Resource entity)
    {
        return CreateSimilar(entity).LoadNotNull(entity);
    }

    protected virtual IResource CreateSimilar(Data.Entities.Resource entity)
    {
        return PathHelper.GetPath(entity.Fullname) switch
        {
            StrictResource.Strict => CreateStrict(entity),
            _ => CreateResource(entity)
        };
    }
    
    private static Resource CreateResource(IEntity entity)
    {
        return new Resource(entity);
    }
    
    private static StrictResource CreateStrict(Data.Entities.Resource entity)
    {
        return new StrictResource(entity);
    }
}