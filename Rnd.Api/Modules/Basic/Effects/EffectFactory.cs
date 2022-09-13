using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Effects;

public class EffectFactory : IStorableFactory<Data.Entities.Effect>
{
    public static IEffect Create(Data.Entities.Effect entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<Data.Entities.Effect> CreateStorable(Data.Entities.Effect entity)
    {
        throw new NotImplementedException();
    }
}