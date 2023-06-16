using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rnd.Constants;

namespace Rnd.Models.Nodes;

public abstract class Field : Node
{
    protected Field(
        string name, 
        string path, 
        Guid? unitId, 
        Guid? methodId
    ) : base(name, path)
    {
        UnitId = unitId;
        MethodId = methodId;
    }

    public Guid? UnitId { get; protected set; }
    public virtual Unit? Unit { get; protected set; }
    public bool IsProperty => UnitId != null;

    public Guid? MethodId { get; protected set; }
    public virtual Method? Method { get; protected set; }
    public bool IsParameter => MethodId != null;

    [MaxLength(TextSize.Tiny)] 
    public abstract Type Type { get; }
    
    [MaxLength(TextSize.Tiny)] 
    public Accessibility Accessibility { get; protected set; } = Accessibility.Space;
    
    [MaxLength(TextSize.Tiny)] 
    public Interactivity Interactivity { get; protected set; } = Interactivity.Editable;
    
    [MaxLength(TextSize.Tiny)] 
    public Enumerating Enumerating { get; protected set; } = Enumerating.None;
    
    public bool Nullable { get; protected set; } = false;
    
    [Column(TypeName = "json")]
    public string? Value { get; protected set; }
    
    public override Prototype Prototype => Prototype.Field;
    public override Guid? ParentId => UnitId;
    public override Node? Parent => Unit ?? Method as Node;
    public override IReadOnlyList<Node> Children => new List<Node>();
}

public enum Type : byte
{
    Object,
    Reference,
    Procedure,
    String,
    Integer,
    Decimal,
    Boolean,
    Select,
    Color,
    Icon,
    Image,
    DateTime,
    Date,
    Time,
    Link,
}

public enum Accessibility : byte
{
    Space,
    Unit,
    Module,
    Global
}

public enum Interactivity : byte
{
    Editable,
    Readonly,
    Hidden,
    Constant,
    Modifiable
}

public enum Enumerating : byte
{
    None,
    Set,
    List,
    Dictionary,
}