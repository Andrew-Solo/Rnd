using Microsoft.EntityFrameworkCore;
using Rnd.Models.Nodes;

namespace Rnd.Models;

[Index(nameof(Name), IsUnique = true)]
public class Space : Model
{
    protected Space(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public Guid OwnerId { get; protected set; }
    public virtual Member Owner { get; protected set; } = null!;
    
    public virtual List<Group> Groups { get; } = new();
    public virtual List<Member> Members { get; } = new();
    public virtual List<Module> Modules { get; } = new();
    public virtual List<Instance> Instances { get; } = new();
    
    public DateTimeOffset? Archived { get; protected set; }
    public bool IsArchived => Archived != null;
}