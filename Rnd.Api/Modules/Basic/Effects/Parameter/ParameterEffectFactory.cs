using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
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
        return entity.Type switch
        {
            nameof(Int32) => CreateInt32(entity),
            _ => throw new ArgumentOutOfRangeException(nameof(entity.Type), entity.Type, 
                Lang.Exceptions.IStorableFactory.UnknownType)
        };
    }
    
    private static Int32ParameterEffect CreateInt32(ParameterEffect entity)
    {
        return new Int32ParameterEffect(EffectFactory.Create(entity.Effect), PathHelper.GetName(entity.ParameterFullname));
    }
}