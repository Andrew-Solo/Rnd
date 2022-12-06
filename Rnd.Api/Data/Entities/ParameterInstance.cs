using System.ComponentModel.DataAnnotations.Schema;

namespace Rnd.Api.Data.Entities;

public class ParameterInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public virtual CharacterInstance Character { get; set; } = null!;
    public virtual ParameterDefinition Definition { get; set; } = null!;
    
    [Column(TypeName = "json")]
    public string Value { get; set; } = null!;
    
    #region Navigation
    
    public Guid CharacterId { get; set; }
    public Guid DefinitionId { get; set; }

    #endregion
}