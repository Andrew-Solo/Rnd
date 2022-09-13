using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Effects.Resource;

public class ResourceEffectFactory : IStorableFactory<Data.Entities.ResourceEffect>
{
    public static IResourceEffect Create(Data.Entities.ResourceEffect entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<Data.Entities.ResourceEffect> CreateStorable(Data.Entities.ResourceEffect entity)
    {
        throw new NotImplementedException();
    }
}