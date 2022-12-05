namespace Rnd.Api.Data.Entities;

public class CharacterInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public virtual Character Character { get; set; } = null!;
    public virtual CharacterDefinition Definition { get; set; } = null!;
    public virtual List<ParameterInstance> Parameters { get; set; } = new();
    
    #region Navigation
    
    public Guid CharacterId { get; set; }
    public Guid DefinitionId { get; set; }

    #endregion
}