using System.ComponentModel.DataAnnotations;
using Rnd.Constants;
using Rnd.Entities.Nodes;

namespace Rnd.Entities;

public class Instance : Model
{
    public Instance(
        string name, 
        string path, 
        Guid spaceId, 
        Guid creatorId, 
        Guid unitId, 
        string value
    ) : base(name, path)
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
    
    [MaxLength(TextSize.Paragraph)] 
    public string Value { get; protected set; }
}