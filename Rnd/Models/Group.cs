using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Ardalis.GuardClauses;
using Rnd.Data;
using Rnd.Primitives;
using Rnd.Results;

namespace Rnd.Models;

public class Group : Model
{
    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;
    
    public virtual List<Member> Members { get; } = new();
    
    public bool DenyAll { get; protected set; }
    public bool DenyAllAsCreator { get; protected set; }
    
    [Column(TypeName = "json")] 
    public List<Permission> Permissions { get; protected set; } = new();
    
    public Group(Guid spaceId)
    {
        SpaceId = spaceId;
    }

    public static Result<Group> Create(GroupData data)
    {
        Guard.Against.Null(data.SpaceId);
        
        var user = new Group(data.SpaceId.Value);
        
        user.FillData(data);
        
        return Result.Ok(user);
    }
    
    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var groupData = (GroupData) data;
        if (groupData.DenyAll != null) DenyAll = groupData.DenyAll.Value;
        if (groupData.DenyAllAsCreator != null) DenyAllAsCreator = groupData.DenyAllAsCreator.Value;
        if (groupData.Permissions != null) Permissions = groupData.Permissions;
    }

    public override ExpandoObject Get()
    {
        dynamic view = base.Get();

        view.spaceId = SpaceId;
        view.denyAll = DenyAll;
        view.denyAllAsCreator = DenyAllAsCreator;
        view.permissions = Permissions;
        
        return view;
    }
}
