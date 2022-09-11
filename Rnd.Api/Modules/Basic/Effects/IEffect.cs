using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Effects.Parameter;
using Rnd.Api.Modules.Basic.Effects.Resource;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;

namespace Rnd.Api.Modules.Basic.Effects;

public interface IEffect : IStorable<Data.Entities.Effect>
{
    public string? Path { get; set; }
    public string Name { get; set; }
    
    public List<IParameterEffect> ParameterEffects { get; }
    public List<IResourceEffect> ResourceEffects { get;}

    public TParameter ModifyParameter<TParameter>(TParameter parameter) where TParameter : IParameter
    {
        return ParameterEffects.Aggregate(parameter, (current, effect) => effect.Modify(current));
    }

    public TResource ModifyResource<TResource>(TResource resource) where TResource : IResource
    {
        return ResourceEffects.Aggregate(resource, (current, effect) => effect.Modify(current));
    }
}