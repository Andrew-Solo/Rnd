namespace Rnd.Api.Data.Entities;

public class ParameterEffect : IEntity
{
    public Guid Id { get; set; }
    
    public virtual Parameter Parameter { get; set; } = null!;
    
    public string ModifierJson { get; set; } = null!;
}