using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Logic.Models.Parameters;

namespace Rnd.Api.Logic.Models.Effects;

public interface IParameterEffect : IStorable<ParameterEffect>
{
    public string? ParameterPath { get; }
    public string ParameterName { get; }
    public object Modifier { get; set; }
    
    public IParameter Modify(IParameter parameter);
}