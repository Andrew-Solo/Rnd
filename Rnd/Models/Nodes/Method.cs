using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Primitives;
using Rnd.Results;

namespace Rnd.Models.Nodes;

public class Method : Node
{
    public Guid UnitId { get; protected set; }
    public virtual Unit Unit { get; protected set; } = null!;
    
    [MaxLength(TextSize.Tiny)] 
    public Methodology Methodology { get; protected set; }
    
    public Guid? ReturnId { get; protected set; }
    public virtual Field? Return { get; protected set; }
    public virtual List<Field> Parameters { get; } = new();
    
    [Column(TypeName = "json")]
    public string? Value { get; protected set; }
    
    public override Prototype Prototype => Prototype.Method;
    public override Guid? ParentId => UnitId;
    public override Node Parent => Unit;
    public override IReadOnlyList<Node> Children => Return != null 
        ? Parameters.Cast<Node>().Union(new []{Return}).ToList() 
        : Parameters.Cast<Node>().ToList();
    
    protected Method(Methodology methodology, Guid unitId)
    {
        Methodology = methodology;
        UnitId = unitId;
    }
    
    public static Result<Method> Create(MethodData data)
    {
        Guard.Against.Null(data.Methodology);
        Guard.Against.Null(data.UnitId);
        
        var module = new Method(data.Methodology.Value, data.UnitId.Value);
        
        module.FillData(data);
        
        return Result.Ok(module);
    }

    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var methodData = (MethodData) data;
        ReturnId = methodData.ReturnId;
        if (methodData.Value != null) Value = methodData.Value;
    }
    
    public override ExpandoObject Get()
    {
        dynamic view = base.Get();
        
        view.methodology = Methodology.ToString();
        view.unitId = UnitId;
        view.value = JsonConvert.DeserializeObject(Value ?? "null")!;
        
        return view;
    }
}