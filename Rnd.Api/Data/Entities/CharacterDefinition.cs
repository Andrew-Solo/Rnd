namespace Rnd.Api.Data.Entities;

public class CharacterDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public virtual Module Module { get; set; } = null!;
    public virtual List<ParameterDefinition> Parameters { get; set; } = new();

    #region Navigation
    
    public Guid ModuleId { get; set; }

    #endregion
}