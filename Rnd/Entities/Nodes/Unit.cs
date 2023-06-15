namespace Rnd.Entities.Nodes;

public class Unit : Node
{
    protected Unit(string name, string path) : base(name, path) { }
    
    public Guid ModuleId { get; protected set; }
    public virtual Module Module { get; protected set; } = null!;

    public virtual List<Instance> Instances { get; protected set; } = new();
    public virtual List<Field> Fields { get; protected set; } = new();
    public virtual List<Method> Methods { get; protected set; } = new();

    public override Prototype Prototype => Prototype.Unit;
    public override Guid? ParentId => ModuleId;
    public override Node Parent => Module;
    public override IReadOnlyList<Node> Children => Fields.Cast<Node>().Union(Methods).ToList();
}