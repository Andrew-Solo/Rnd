using System.ComponentModel.DataAnnotations;
using Rnd.Constants;

namespace Rnd.Models.Nodes;

public abstract class Method : Node
{
    protected Method(
        string name, 
        string path, 
        Guid unitId
    ) : base(name, path)
    {
        UnitId = unitId;
    }

    public Guid UnitId { get; protected set; }
    public virtual Unit Unit { get; protected set; } = null!;
    
    [MaxLength(TextSize.Tiny)] 
    public abstract Methodology Methodology { get; }
    
    public Guid? ReturnId { get; protected set; }
    public virtual Field? Return { get; protected set; }
    public List<Field> Parameters { get; protected set; } = new();
    
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