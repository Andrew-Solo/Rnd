namespace Rnd.Api.Data.Entities;

public class ParameterEffect : IEntity
{
    public Guid Id { get; set; }
    public string ParameterFullname { get; set; } = null!;
    public string ModifierJson { get; set; } = null!;
    
    public Guid EffectId { get; set; }
}