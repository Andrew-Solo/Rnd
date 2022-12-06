namespace Rnd.Api.Data.Entities;

public class Module
{
    #region Factories

    protected Module() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();    
    public string Name { get; set; } = null!;
    public string Version { get; set; } = "1.0.0";
    
    public virtual Module? Parent { get; protected set; } = null!;
    public virtual List<Module> Children { get; protected set; } = new();

    public virtual List<Type> Types { get; protected set; } = new();
    public virtual CharacterDefinition CharacterDefinition { get; protected set; } = null!;
    
    #region Navigation
    
    public virtual Guid? ParentId { get; protected set; }

    #endregion
}