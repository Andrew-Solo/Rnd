using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Effects.Resource;

public class ResourceEffectFactory : IStorableFactory<Data.Entities.ResourceEffect>
{
    public static IResourceEffect Create(Data.Entities.ResourceEffect entity)
    {
        var factory = new ResourceEffectFactory();
        return (IResourceEffect) factory.CreateStorable(entity);
    }

    public IStorable<Data.Entities.ResourceEffect> CreateStorable(Data.Entities.ResourceEffect entity)
    {
        var result = new ResourceEffect(entity);
        result.Load(entity);
        return result;
    }
}