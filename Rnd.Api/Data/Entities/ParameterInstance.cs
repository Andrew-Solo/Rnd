namespace Rnd.Api.Data.Entities;

public class ParameterInstance
{
    public Guid Id { get; set; }
    public virtual CharacterInstance Character { get; set; } = null!;
    public virtual ParameterDefinition Definition { get; set; } = null!;
    public string Value { get; set; } = null!;
    
    #region Navigation
    
    public Guid CharacterId { get; set; }
    public Guid DefinitionId { get; set; }

    #endregion
}