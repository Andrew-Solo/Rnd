namespace Rnd.Api.Data.Entities;

public class CharacterDefinition
{
    #region Factories

    protected CharacterDefinition() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public virtual Module Module { get; protected set; } = null!;
    public virtual List<ParameterDefinition> Parameters { get; protected set; } = new();

    #region Navigation
    
    public Guid ModuleId { get; protected set; }

    #endregion
}