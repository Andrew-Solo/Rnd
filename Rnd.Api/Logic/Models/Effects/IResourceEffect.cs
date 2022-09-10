using Rnd.Api.Data;
using Rnd.Api.Logic.Models.Resources;

namespace Rnd.Api.Logic.Models.Effects;

public interface IResourceEffect : IStorable<Data.Entities.ResourceEffect>
{
    public string? ResourcePath { get; set; }
    public string ResourceName { get; set; }
    
    public decimal? ValueModifier { get; set; }
    public decimal? MinModifier { get; set; }
    public decimal? MaxModifier { get; set; }
    
    public IResource Modify(IResource parameter);
}