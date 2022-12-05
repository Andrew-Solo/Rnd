namespace Rnd.Api.Data.Entities;

public class Module
{
    public Guid Id { get; set; } = Guid.NewGuid();    
    public string Name { get; set; } = null!;
    public Version Version { get; set; } = new(1, 0);
    
    public virtual Module Parent { get; set; } = null!;
    public virtual List<Module> Children { get; set; } = new();

    public virtual List<Type> Types { get; set; } = new();
    public virtual CharacterDefinition CharacterDefinition { get; set; } = null!;
    
    #region Navigation
    
    public virtual CharacterDefinition CharacterDefinitionId { get; set; } = null!;

    #endregion
}