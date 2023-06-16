using Rnd.Constants;

namespace Rnd.Models;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTimeOffset Created { get; protected set; } = Time.Now;
    public DateTimeOffset Viewed { get; protected set; } = Time.Now;
    public DateTimeOffset? Updated { get; protected set; }
    public DateTimeOffset? Deleted { get; protected set; }
    public bool IsDeleted => Deleted != null;
}