using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Primitives;
using Rnd.Results;
using Type = Rnd.Primitives.Type;

namespace Rnd.Models.Nodes;

public class Field : Node
{
    protected Field(Type type, Guid? unitId, Guid? methodId)
    {
        Type = type;
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
    public Type Type { get; protected set; }
    
    [MaxLength(TextSize.Tiny)] 
    public Accessibility Accessibility { get; protected set; } = Accessibility.Space;
    
    [MaxLength(TextSize.Tiny)] 
    public Interactivity Interactivity { get; protected set; } = Interactivity.Editable;
    
    [MaxLength(TextSize.Tiny)] 
    public Enumerating Enumerating { get; protected set; } = Enumerating.None;
    
    public bool Nullable { get; protected set; }
    
    [Column(TypeName = "json")]
    public string? Value { get; protected set; }
    
    public override Prototype Prototype => Prototype.Field;
    public override Guid? ParentId => UnitId;
    public override Node? Parent => Unit ?? Method as Node;
    public override IReadOnlyList<Node> Children => new List<Node>();
    
    public static Result<Field> Create(FieldData data)
    {
        Guard.Against.Null(data.Type);
        if (data.MethodId == null) Guard.Against.Null(data.UnitId);
        if (data.UnitId == null) Guard.Against.Null(data.MethodId);
        if (data.UnitId != null && data.MethodId != null) throw new ArgumentException("Node cannot have two parents");
        
        var module = new Field(data.Type.Value, data.UnitId, data.MethodId);
        
        module.FillData(data);
        
        return Result.Ok(module);
    }

    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var fieldData = (FieldData) data;
        if (fieldData.Accessibility != null) Accessibility = fieldData.Accessibility.Value;
        if (fieldData.Interactivity != null) Interactivity = fieldData.Interactivity.Value;
        if (fieldData.Enumerating != null) Enumerating = fieldData.Enumerating.Value;
        if (fieldData.Nullable != null) Nullable = fieldData.Nullable.Value;
        if (fieldData.Value != null) Value = fieldData.Value;
    }
    
    public override ExpandoObject Get()
    {
        dynamic view = base.Get();

        view.type = Type.ToString();
        view.unitId = UnitId!;
        view.methodId = MethodId!;
        view.accessibility = Accessibility.ToString();
        view.interactivity = Interactivity.ToString();
        view.enumerating = Enumerating.ToString();
        view.nullable = Nullable;
        view.value = JsonConvert.DeserializeObject(Value ?? "null")!;
        
        return view;
    }
}