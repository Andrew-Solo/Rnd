using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Resources;

namespace Rnd.Api.Modules.Basic.Effects.Resource;

public interface IResourceEffect : IStorable<Data.Entities.ResourceEffect>
{
    public Guid EffectId { get; }
    public string? ResourcePath { get; set; }
    public string ResourceName { get; set; }
    
    public decimal? ValueModifier { get; set; }
    public decimal? MinModifier { get; set; }
    public decimal? MaxModifier { get; set; }

    public TResource Modify<TResource>(TResource resource) where TResource : IResource;
}