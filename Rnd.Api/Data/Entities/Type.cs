namespace Rnd.Api.Data.Entities;

public class Type
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public virtual Module? Module { get; set; }
    public string Name { get; set; } = null!;
    public virtual List<ParameterDefinition> Parameters { get; set; } = new();

    #region Navigation

    public virtual List<ParameterDefinition> UsingParameters { get; set; } = new();
    public Guid? ModuleId { get; set; }

    #endregion
}