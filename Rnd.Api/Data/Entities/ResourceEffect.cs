namespace Rnd.Api.Data.Entities;

public class ResourceEffect
{
    public Guid Id { get; set; }
    
    public virtual Resource Parameter { get; set; } = null!;
    
    public int? ValueModifier { get; set; } = null!;
    
    public int? MinModifier { get; set; } = null!;
    
    public int? MaxModifier { get; set; } = null!;
}