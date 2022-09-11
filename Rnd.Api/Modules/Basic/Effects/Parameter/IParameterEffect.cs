using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.Basic.Effects.Parameter;

public interface IParameterEffect : IStorable<ParameterEffect>
{
    public string? ParameterPath { get; }
    public string ParameterName { get; }
    public object Modifier { get; set; }

    public TParameter Modify<TParameter>(TParameter parameter) where TParameter : IParameter;
}