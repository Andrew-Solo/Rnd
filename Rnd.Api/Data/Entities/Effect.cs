using System.ComponentModel.DataAnnotations;

namespace Rnd.Api.Data.Entities;

public class Effect : IEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Fullname { get; set; } = null!;
    
    public virtual List<ParameterEffect> ParameterEffects { get; set; } = new();
    
    public virtual List<ResourceEffect> ResourceEffects { get; set; } = new();
}