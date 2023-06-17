using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Rnd.Data;
using Rnd.Results;

namespace Rnd.Models.Nodes;

public class Module : Node
{
    protected Module(
        string path,
        string name,
        Guid mainId
    ) : base(path, name)
    {
        MainId = mainId;
    }

    [Column(TypeName = "json")]
    public Version Version { get; protected set; } = new("0.1.0");
    
    public bool System { get; protected set; } = false;
    public bool Default { get; protected set; } = false;
    public bool Hidden { get; protected set; } = false;

    public Guid MainId { get; protected set; }
    public virtual Unit Main { get; protected set; } = null!;
    public virtual List<Unit> Units { get; } = new();
    public virtual List<Space> Spaces { get; } = new();

    public override Prototype Prototype => Prototype.Module;
    public override Guid? ParentId => null;
    public override Node? Parent => null;
    public override IReadOnlyList<Node> Children => Units.Cast<Node>().Union(new []{Main}).ToList();
    
    public static Result<Module> Create(ModuleData data)
    {
        Guard.Against.Null(data.Path);
        Guard.Against.Null(data.Name);
        Guard.Against.Null(data.MainId);
        
        var module = new Module(data.Path, data.Name, data.MainId.Value);
        
        module.FillModel(data);
        
        return Result.Ok(module);
    }

    protected override void FillModel(ModelData data)
    {
        base.FillModel(data);
        var moduleData = (ModuleData) data;
        if (moduleData.Version != null) Version = moduleData.Version;
        if (moduleData.System != null) System = moduleData.System.Value;
        if (moduleData.Default != null) Default = moduleData.Default.Value;
        if (moduleData.Hidden != null) Hidden = moduleData.Hidden.Value;
    }
}