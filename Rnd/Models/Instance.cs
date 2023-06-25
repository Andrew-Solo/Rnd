using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Ardalis.GuardClauses;
using Rnd.Data;
using Rnd.Results;

namespace Rnd.Models;

public class Instance : Model
{
    public Guid CreatorId { get; protected set; }
    public virtual Member Creator { get; protected set; } = null!;
    
    public Guid UnitId { get; protected set; }
    public virtual Unit Unit { get; protected set; } = null!;
    
    public Guid? SpaceId { get; protected set; }
    public virtual Space? Space { get; protected set; }
    
    [Column(TypeName = "json")]
    public Dictionary<string, string> Properties { get; protected set; } = new();
    
    public Instance(Guid creatorId, Guid unitId)
    {
        CreatorId = creatorId;
        UnitId = unitId;
    }
    
    public static Result<Instance> Create(InstanceData data)
    {
        Guard.Against.Null(data.CreatorId);
        Guard.Against.Null(data.UnitId);
        
        var instance = new Instance(data.CreatorId.Value, data.UnitId.Value);
        
        instance.FillData(data);
        
        return Result.Ok(instance);
    }

    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var instanceData = (InstanceData) data;
        SpaceId = instanceData.SpaceId;
        Properties.Merge(instanceData.Properties);
    }
    
    public override ExpandoObject Get()
    {
        dynamic view = base.Get();
        
        view.creatorId = CreatorId;
        view.unitId = UnitId;
        view.spaceId = SpaceId!;
        view.properties = Properties;
        
        return view;
    }
}