namespace Rnd.Api.Data.Entities;

public class ResourceEffect
{
    public Guid Id { get; set; }
    
    public virtual Resource Resource { get; set; } = null!;
    
    public decimal? ValueModifier { get; set; } = null!;
    
    public decimal? MinModifier { get; set; } = null!;
    
    public decimal? MaxModifier { get; set; } = null!;
}