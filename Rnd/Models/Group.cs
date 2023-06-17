using System.ComponentModel.DataAnnotations.Schema;
using Rnd.Primitives;

namespace Rnd.Models;

public class Group : Model
{
    public Group(
        string path, 
        string name,
        Guid spaceId
    ) : base(path, name)
    {
        SpaceId = spaceId;
    }

    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;
    
    public virtual List<Member> Members { get; } = new();
    
    public bool DenyAll { get; protected set; } = false;
    public bool DenyAllAsCreator { get; protected set; } = false;
    
    [Column(TypeName = "json")] 
    public List<Permission> Permissions { get; protected set; } = new();
}
