namespace Rnd.Models;

public class Member : Model
{
    protected Member(Guid userId, Guid spaceId)
    {
        UserId = userId;
        SpaceId = spaceId;
    }
    
    public Guid UserId { get; protected set; }
    public virtual User User { get; protected set; } = null!;
    
    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;
    
    public virtual List<Group> Groups { get; } = new();

    public DateTimeOffset? Active { get; protected set; }
    public DateTimeOffset? Banned { get; protected set; }
    public bool IsBanned => Banned != null;
}