using System.Dynamic;
using Ardalis.GuardClauses;
using Rnd.Data;
using Rnd.Results;

namespace Rnd.Models.Nodes;

public class Unit : Node
{
    protected Unit(Guid moduleId)
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
    
    public static Result<Unit> Create(UnitData data)
    {
        Guard.Against.Null(data.ModuleId);
        
        var module = new Unit(data.ModuleId.Value);
        
        module.FillData(data);
        
        return Result.Ok(module);
    }

    protected override void FillData(ModelData data)
    {
        base.FillData(data);
    }
    
    public override ExpandoObject Get()
    {
        dynamic view = base.Get();
        
        view.ModuleId = ModuleId;

        return view;
    }
}