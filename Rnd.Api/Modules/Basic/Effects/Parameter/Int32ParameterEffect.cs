namespace Rnd.Api.Modules.Basic.Effects.Parameter;

public class Int32ParameterEffect : ParameterEffect<Int32>
{
    public Int32ParameterEffect(IEffect effect, string parameterName, int modifier) : base(effect, parameterName, modifier)
    {
        
    }

    public override TParameter Modify<TParameter>(TParameter parameter)
    {
        throw new NotImplementedException();
    }
}