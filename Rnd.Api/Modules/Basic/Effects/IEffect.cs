using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;

namespace Rnd.Api.Modules.Basic.Effects;

public interface IEffect : IStorable<Data.Entities.Effect>
{
    public string? Path { get; set; }
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