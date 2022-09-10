using Rnd.Api.Models.Parameters;

namespace Rnd.Api.Models.Effects;

public interface IParameterEffect
{
    public string ParameterGroup { get; }
    public string ParameterName { get; }
    public object Modifier { get; set; }
    
    public IParameter Modify(IParameter parameter);
}