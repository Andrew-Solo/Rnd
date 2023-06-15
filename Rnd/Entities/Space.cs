﻿namespace Rnd.Entities;

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
    
    public virtual List<Member> Members { get; protected set; } = new();
    public virtual List<Plugin> Plugins { get; protected set; } = new();
    public virtual List<Instance> Instances { get; protected set; } = new();
    
    public DateTimeOffset? Archived { get; protected set; }
    public bool IsArchived => Archived != null;
}