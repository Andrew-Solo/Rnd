using System.Dynamic;
using Ardalis.GuardClauses;
using Rnd.Data;
using Rnd.Results;

namespace Rnd.Models;

public class Member : Model
{
    public Guid UserId { get; protected set; }
    public virtual User User { get; protected set; } = null!;
    
    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;
    
    public virtual List<Group> Groups { get; } = new();

    public DateTimeOffset? Active { get; protected set; }
    public DateTimeOffset? Banned { get; protected set; }
    public bool IsBanned => Banned != null;
    
    protected Member(Guid userId, Guid spaceId)
    {
        UserId = userId;
        SpaceId = spaceId;
    }

    public static Result<Member> Create(MemberData data)
    {
        Guard.Against.Null(data.UserId);
        Guard.Against.Null(data.SpaceId);
        
        var user = new Member(data.UserId.Value, data.SpaceId.Value);
        
        user.FillData(data);
        
        return Result.Ok(user);
    }
    
    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var memberData = (MemberData) data;
        Active = memberData.Active;
        Banned = memberData.Banned;
    }

    public override ExpandoObject Get()
    {
        dynamic view = base.Get();

        view.userId = UserId;
        view.spaceId = SpaceId;
        view.active = Active!;
        view.banned = Banned!;
        
        return view;
    }
}