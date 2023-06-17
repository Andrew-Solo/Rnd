using System.ComponentModel.DataAnnotations.Schema;
using Rnd.Models.Nodes;

namespace Rnd.Models;

public class Instance : Model
{
    public Instance(Guid spaceId, Guid creatorId, Guid unitId, string value)
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