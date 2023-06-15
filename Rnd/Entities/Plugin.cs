using Rnd.Entities.Nodes;

namespace Rnd.Entities;

public class Plugin : Entity
{
    protected Plugin(Guid spaceId, Guid moduleId)
    {
        SpaceId = spaceId;
        ModuleId = moduleId;
    }

    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;
    
    public Guid ModuleId { get; protected set; }
    public virtual Module Module { get; protected set; } = null!;
}