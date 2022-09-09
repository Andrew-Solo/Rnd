namespace Rnd.Api.Data.Entities;

public class Effect
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual List<ParameterEffect> ParameterEffects { get; set; } = new();
    public virtual List<ResourceEffect> ResourceEffects { get; set; } = new();
}