using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.Basic.Effects.Parameter;

public class Int32ParameterEffect : ParameterEffect<Int32>
{
    public Int32ParameterEffect(IEntity entity) : base(entity) { }
    public Int32ParameterEffect(IEffect effect, string parameterName, int modifier = 0) 
        : base(effect, parameterName, modifier)
    {
        
    }

    public override TParameter Modify<TParameter>(TParameter parameter)
    {
        if (parameter is Int32Parameter int32)
        {
            int32.Value += Modifier;
        }

        return parameter;
    }
}