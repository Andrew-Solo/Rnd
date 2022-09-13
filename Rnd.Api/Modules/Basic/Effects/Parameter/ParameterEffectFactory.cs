using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Modules.Basic.Effects.Parameter;

public class ParameterEffectFactory : IStorableFactory<ParameterEffect>
{
    public static IParameterEffect Create(ParameterEffect entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<ParameterEffect> CreateStorable(ParameterEffect entity)
    {
        throw new NotImplementedException();
    }
}