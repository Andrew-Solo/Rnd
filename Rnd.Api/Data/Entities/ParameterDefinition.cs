using System.ComponentModel.DataAnnotations.Schema;

namespace Rnd.Api.Data.Entities;

public class ParameterDefinition
{
    #region Factories

    protected ParameterDefinition() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public virtual CharacterDefinition Source { get; protected set; } = null!;
    
    public virtual Group Group { get; protected set; } = null!;
    public string Name { get; protected set; } = null!;
    public virtual Type Type { get; protected set; } = null!;
    
    [Column(TypeName = "json")]
    public string Default { get; protected set; } = null!;
    
    public bool Const { get; protected set; }
    public bool Nullable { get; protected set; }
    public bool Hidden { get; protected set; }
    public bool Disabled { get; protected set; }
    
    [Column(TypeName = "json")]
    public Dictionary<string, string> Attributes { get; protected set; } = new();
    
    #region Navigation
    
    public Guid SourceId { get; protected set; }
    public Guid GroupId { get; protected set; }

    public Guid TypeId { get; protected set; }
    public virtual List<ParameterInstance> Instances { get; protected set; } = new();
    public virtual List<Type> UsingTypes { get; protected set; } = new();

    #endregion
}