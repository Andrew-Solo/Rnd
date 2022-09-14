using System.ComponentModel.DataAnnotations;

namespace Rnd.Api.Data.Entities;

public class ParameterEffect : IEntity
{
    public Guid Id { get; set; }
    public Guid EffectId { get; set; }
    public string ParameterFullname { get; set; } = null!;
    
    [MaxLength(511)]
    public string Type { get; set; } = nameof(Int32);
    
    public string ModifierJson { get; set; } = null!;
    
    
    #region Navigation

    public virtual Effect Effect { get; set; } = null!;

    #endregion
}