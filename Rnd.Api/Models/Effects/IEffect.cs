using Rnd.Api.Models.Parameters;
using Rnd.Api.Models.Resources;

namespace Rnd.Api.Models.Effects;

public interface IEffect
{
    public string Group { get; set; }
    public string Name { get; set; }
    
    public List<IParameterEffect> ParameterEffects { get; }
    public List<IResourceEffect> ResourceEffects { get;}

    public IParameter Modify(IParameter parameter)
    {
        return ParameterEffects.Aggregate(parameter, (current, effect) => effect.Modify(current));
    }

    public IResource Modify(IResource resource)
    {
        return ResourceEffects.Aggregate(resource, (current, effect) => effect.Modify(current));
    }
}