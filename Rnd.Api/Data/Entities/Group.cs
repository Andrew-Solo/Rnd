namespace Rnd.Api.Data.Entities;

public class Group
{
    #region Factories

    protected Group() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public virtual Group? Parent { get; protected set; }
    public virtual List<Group> Children { get; protected set; } = new();
    
    #region Navigation

    public Guid? ParentId { get; protected set; }
    public virtual List<ParameterDefinition> Parameters { get; protected set; } = new();

    #endregion
}