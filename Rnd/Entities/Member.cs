using System.ComponentModel.DataAnnotations.Schema;

namespace Rnd.Entities;

public class Member : Model
{
    protected Member(
        string name, 
        string path, 
        Guid userId, 
        Guid spaceId
    ) : base(name, path)
    {
        UserId = userId;
        SpaceId = spaceId;
    }
    
    public Guid UserId { get; protected set; }
    public virtual User User { get; protected set; } = null!;
    
    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;

    public bool Blacklist { get; protected set; } = false;
    
    [Column(TypeName = "json")]
    public List<string> Permissions { get; protected set; } = new();

    public DateTimeOffset? Active { get; protected set; }
    public DateTimeOffset? Banned { get; protected set; }
    public bool IsBanned => Banned != null;
}