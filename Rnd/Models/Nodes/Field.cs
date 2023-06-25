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

public class Field : Model
{
    public Guid UnitId { get; protected set; }
    public virtual Unit Unit { get; protected set; } = null!;

    [MaxLength(TextSize.Tiny)] 
    public Prototype Prototype { get; protected set; } = Prototype.Property;

    [MaxLength(TextSize.Tiny)] 
    public Type Type { get; protected set; } = Type.Object;

    [MaxLength(TextSize.Tiny)] 
    public Enumerating Enumerating { get; protected set; } = Enumerating.None;
    
    [MaxLength(TextSize.Tiny)] 
    public Accessibility Accessibility { get; protected set; } = Accessibility.Space;
    
    public bool Readonly { get; protected set; }
    public bool Hidden { get; protected set; }
    public bool Modifiable { get; protected set; }
    public bool Nullable { get; protected set; }
    
    [Column(TypeName = "json")]
    public string? Value { get; protected set; }
    
    protected Field(Guid unitId)
    {
        UnitId = unitId;
    }
    
    public static Result<Field> Create(FieldData data)
    {
        Guard.Against.Null(data.UnitId);
        
        var module = new Field(data.UnitId.Value);
        
        module.FillData(data);
        
        return Result.Ok(module);
    }

    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var fieldData = (FieldData) data;
        if (fieldData.Prototype != null) Prototype = fieldData.Prototype.Value;
        if (fieldData.Type != null) Type = fieldData.Type.Value;
        if (fieldData.Enumerating != null) Enumerating = fieldData.Enumerating.Value;
        if (fieldData.Accessibility != null) Accessibility = fieldData.Accessibility.Value;
        if (fieldData.Readonly != null) Readonly = fieldData.Readonly.Value;
        if (fieldData.Hidden != null) Nullable = fieldData.Hidden.Value;
        if (fieldData.Modifiable != null) Nullable = fieldData.Modifiable.Value;
        if (fieldData.Nullable != null) Nullable = fieldData.Nullable.Value;
        if (fieldData.Value != null) Value = fieldData.Value;
    }
    
    public override ExpandoObject Get()
    {
        dynamic view = base.Get();

        view.unitId = UnitId;
        view.prototype = Prototype.ToString();
        view.type = Type.ToString();
        view.enumerating = Enumerating.ToString();
        view.accessibility = Accessibility.ToString();
        view.@readonly = Readonly;
        view.hidden = Hidden;
        view.modifiable = Modifiable;
        view.nullable = Nullable;
        view.value = JsonConvert.DeserializeObject(Value ?? "null")!;
        
        return view;
    }
}