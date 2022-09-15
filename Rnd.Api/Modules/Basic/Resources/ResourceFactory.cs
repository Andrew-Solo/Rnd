using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.RndCore.Characters;
using Rnd.Api.Modules.RndCore.Resources;
using Rnd.Api.Modules.RndCore.Resources.StateResources;
using Path = Rnd.Api.Helpers.Path;

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
        return Path.GetPath(entity.Fullname) switch
        {
            nameof(State) => CreateState(entity),
            nameof(Leveling) => Path.GetName(entity.Fullname) switch
            {
                nameof(Drama) => CreateDrama(entity),
                _ => CreateResource(entity)
            },
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
    
    private static State CreateState(Data.Entities.Resource entity)
    {
        return new State(entity);
    }
    
    private static Drama CreateDrama(Data.Entities.Resource entity)
    {
        return new Drama(entity);
    }
}