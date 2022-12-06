using System.ComponentModel.DataAnnotations.Schema;

namespace Rnd.Api.Data.Entities;

public class ParameterInstance
{
    #region Factories

    protected ParameterInstance() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public virtual CharacterInstance Character { get; protected set; } = null!;
    public virtual ParameterDefinition Definition { get; protected set; } = null!;
    
    [Column(TypeName = "json")]
    public string Value { get; set; } = null!;
    
    #region Navigation
    
    public Guid CharacterId { get; protected set; }
    public Guid DefinitionId { get; protected set; }

    #endregion
}