using System.ComponentModel.DataAnnotations.Schema;

namespace Rnd.Models.Nodes;

public class Module : Node
{
    protected Module(
        string name, 
        string path, 
        Guid mainId
    ) : base(name, path)
    {
        MainId = mainId;
    }

    [Column(TypeName = "json")]
    public Version Version { get; protected set; } = new(0, 1, 0);
    
    public bool System { get; protected set; } = false;
    public bool Default { get; protected set; } = false;
    public bool Hidden { get; protected set; } = false;

    public Guid MainId { get; protected set; }
    public virtual Unit Main { get; protected set; } = null!;
    public virtual List<Unit> Units { get; protected set; } = new();
    public virtual List<Space> Spaces { get; protected set; } = new();

    public override Prototype Prototype => Prototype.Module;
    public override Guid? ParentId => null;
    public override Node? Parent => null;
    public override IReadOnlyList<Node> Children => Units.Cast<Node>().Union(new []{Main}).ToList();
}