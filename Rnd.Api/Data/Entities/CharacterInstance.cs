namespace Rnd.Api.Data.Entities;

public class CharacterInstance
{
    #region Factories

    protected CharacterInstance() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public virtual Character Character { get; protected set; } = null!;
    public virtual CharacterDefinition Definition { get; protected set; } = null!;
    public virtual List<ParameterInstance> Parameters { get; protected set; } = new();
    
    #region Navigation
    
    public Guid CharacterId { get; protected set; }
    public Guid DefinitionId { get; protected set; }

    #endregion
}