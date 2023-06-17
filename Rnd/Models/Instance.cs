using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rnd.Constants;
using Rnd.Models.Nodes;

namespace Rnd.Models;

public class Instance : Model
{
    public Instance(
        string path, 
        string name, 
        Guid spaceId, 
        Guid creatorId, 
        Guid unitId, 
        string value
    ) : base(path, name)
    {
        SpaceId = spaceId;
        CreatorId = creatorId;
        UnitId = unitId;
        Value = value;
    }
    
    public Guid SpaceId { get; protected set; }
    public virtual Space Space { get; protected set; } = null!;
    
    public Guid CreatorId { get; protected set; }
    public virtual Member Creator { get; protected set; } = null!;
    
    public Guid UnitId { get; protected set; }
    public virtual Unit Unit { get; protected set; } = null!;
    
    [Column(TypeName = "json")]
    public string Value { get; protected set; }
}