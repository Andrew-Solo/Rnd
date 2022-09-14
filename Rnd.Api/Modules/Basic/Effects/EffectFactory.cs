using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Modules.RndCore.Effects;

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
        return CreateSimilar(entity).LoadNotNull(entity);
    }
    
    protected virtual IEffect CreateSimilar(Data.Entities.Effect entity)
    {
        return PathHelper.GetPath(entity.Fullname) switch
        {
            nameof(Custom) => CreateCustom(entity),
            _ => CreateEffect(entity)
        };
    }

    private IEffect CreateEffect(IEntity entity)
    {
        return new Effect(entity);
    }
    
    private IEffect CreateCustom(IEntity entity)
    {
        return new Custom(entity);
    }
}