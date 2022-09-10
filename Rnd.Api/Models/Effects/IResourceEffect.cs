using Rnd.Api.Models.Resources;

namespace Rnd.Api.Models.Effects;

public interface IResourceEffect
{
    public string ResourceGroup { get; set; }
    public string ResourceName { get; set; }
    
    public decimal? ValueModifier { get; set; }
    public decimal? MinModifier { get; set; }
    public decimal? MaxModifier { get; set; }
    
    public IResource Modify(IResource parameter);
}