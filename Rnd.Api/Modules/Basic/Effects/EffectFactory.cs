using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;

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
        var result = new Effect(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
        result.Load(entity);
        return result;
    }
}