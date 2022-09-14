using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Localization;

namespace Rnd.Api.Modules.Basic.Effects.Parameter;

public class ParameterEffectFactory : IStorableFactory<ParameterEffect>
{
    public static IParameterEffect Create(ParameterEffect entity)
    {
        var factory = new ParameterEffectFactory();
        return (IParameterEffect) factory.CreateStorable(entity);
    }

    public IStorable<ParameterEffect> CreateStorable(ParameterEffect entity)
    {
        return CreateSimilar(entity).LoadNotNull(entity);
    }
    
    protected virtual IParameterEffect CreateSimilar(ParameterEffect entity)
    {
        return entity.Type switch
        {
            nameof(Int32) => CreateInt32(entity),
            _ => throw new ArgumentOutOfRangeException(nameof(entity.Type), entity.Type, 
                Lang.Exceptions.IStorableFactory.UnknownType)
        };
    }
    
    private static Int32ParameterEffect CreateInt32(IEntity entity)
    {
        return new Int32ParameterEffect(entity);
    }
}