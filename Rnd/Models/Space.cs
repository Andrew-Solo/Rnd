using System.Dynamic;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rnd.Data;
using Rnd.Models.Nodes;
using Rnd.Results;

namespace Rnd.Models;

[Index(nameof(Name), IsUnique = true)]
public class Space : Model
{
    public Guid OwnerId { get; protected set; }
    public virtual Member Owner { get; protected set; } = null!;
    
    public virtual List<Group> Groups { get; } = new();
    public virtual List<Member> Members { get; } = new();
    public virtual List<Module> Modules { get; } = new();
    public virtual List<Instance> Instances { get; } = new();
    
    public DateTimeOffset? Archived { get; protected set; }
    public bool IsArchived => Archived != null;
    
    protected Space(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public static Result<Space> Create(SpaceData data)
    {
        Guard.Against.Null(data.OwnerId);
        
        var user = new Space(data.OwnerId.Value);
        
        user.FillData(data);
        
        return Result.Ok(user);
    }
    
    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var spaceData = (SpaceData) data;
        Archived = spaceData.Archived;
    }

    public override ExpandoObject Get()
    {
        dynamic view = base.Get();
        
        view.ownerId = OwnerId;
        view.archived = Archived!;
        
        return view;
    }
}