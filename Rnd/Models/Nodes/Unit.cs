namespace Rnd.Models.Nodes;

public class Unit : Node
{
    protected Unit(
        string path, 
        string name,
        Guid moduleId
    ) : base(path, name)
    {
        ModuleId = moduleId;
    }
    
    public Guid ModuleId { get; protected set; }
    public virtual Module Module { get; protected set; } = null!;

    public virtual List<Instance> Instances { get; } = new();
    public virtual List<Field> Fields { get; } = new();
    public virtual List<Method> Methods { get; } = new();

    public override Prototype Prototype => Prototype.Unit;
    public override Guid? ParentId => ModuleId;
    public override Node Parent => Module;
    public override IReadOnlyList<Node> Children => Fields.Cast<Node>().Union(Methods).ToList();
}