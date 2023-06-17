using System.ComponentModel.DataAnnotations;
using Rnd.Constants;

namespace Rnd.Models.Nodes;

public class Method : Node
{
    protected Method(
        string path, 
        string name,
        Methodology methodology,
        Guid unitId
    ) : base(path, name)
    {
        Methodology = methodology;
        UnitId = unitId;
    }

    public Guid UnitId { get; protected set; }
    public virtual Unit Unit { get; protected set; } = null!;
    
    [MaxLength(TextSize.Tiny)] 
    public Methodology Methodology { get; protected set; }
    
    public Guid? ReturnId { get; protected set; }
    public virtual Field? Return { get; protected set; }
    public virtual List<Field> Parameters { get; } = new();
    
    public override Prototype Prototype => Prototype.Method;
    public override Guid? ParentId => UnitId;
    public override Node Parent => Unit;
    public override IReadOnlyList<Node> Children => Return != null 
        ? Parameters.Cast<Node>().Union(new []{Return}).ToList() 
        : Parameters.Cast<Node>().ToList();
}

public enum Methodology : byte
{
    Function,
    Action
}