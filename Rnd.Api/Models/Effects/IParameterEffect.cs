using Rnd.Api.Models.Parameters;

namespace Rnd.Api.Models.Effects;

public interface IParameterEffect
{
    public Guid Id { get; }
    public string ParameterGroup { get; }
    public string ParameterName { get; }
    public object Modifier { get; set; }
    
    public IParameter Modify(IParameter parameter);
}