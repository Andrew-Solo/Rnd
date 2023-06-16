using System.ComponentModel.DataAnnotations.Schema;

namespace Rnd.Models;

public class Group : Model
{
    public Group(string name, string path) : base(name, path) { }

    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;
    
    public virtual List<Member> Members { get; protected set; } = new();

    public bool DenyByDefault { get; protected set; } = false;
    public bool DenyAsCreator { get; protected set; } = false;
    
    [Column(TypeName = "json")] 
    public List<Permission> Permissions { get; protected set; } = new();
}

public record struct Permission(
    string Path,
    bool Allow = true,
    bool AllowAsCreator = true
);
