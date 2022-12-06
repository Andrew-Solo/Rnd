namespace Rnd.Api.Data.Entities;

public class Group
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public virtual Group? Parent { get; set; }
    public virtual List<Group> Children { get; set; } = new();
    
    #region Navigation

    public Guid? ParentId { get; set; }
    public virtual List<ParameterDefinition> Parameters { get; set; } = new();

    #endregion
}