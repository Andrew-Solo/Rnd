using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Effects;

public class EffectFactory : IStorableFactory<Data.Entities.Effect>
{
    public static IEffect Create(Data.Entities.Effect entity)
    {
        var factory = new EffectFactory();
        return (IEffect) factory.CreateStorable(entity);
    }

    public IStorable<Data.Entities.Effect> CreateStorable(Data.Entities.Effect entity)
    {
        var result = new Effect(entity);
        result.Load(entity);
        return result;
    }
}