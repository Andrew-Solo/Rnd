using Rnd.Models.Nodes;

namespace Rnd.Models;

public class Space : Model
{
    protected Space(
        string name, 
        string path, 
        Guid ownerId
    ) : base(name, path)
    {
        OwnerId = ownerId;
    }

    public Guid OwnerId { get; protected set; }
    public virtual Member Owner { get; protected set; } = null!;
    
    public virtual List<Group> Groups { get; protected set; } = new();
    public virtual List<Member> Members { get; protected set; } = new();
    public virtual List<Module> Modules { get; protected set; } = new();
    public virtual List<Instance> Instances { get; protected set; } = new();
    
    public DateTimeOffset? Archived { get; protected set; }
    public bool IsArchived => Archived != null;
}