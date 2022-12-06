namespace Rnd.Api.Data.Entities;

public class Type
{
    #region Factories

    protected Type() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public virtual Module? Module { get; protected set; }
    public string Name { get; protected set; } = null!;
    public virtual List<ParameterDefinition> Parameters { get; protected set; } = new();

    #region Navigation

    public virtual List<ParameterDefinition> UsingParameters { get; protected set; } = new();
    public Guid? ModuleId { get; protected set; }

    #endregion
}