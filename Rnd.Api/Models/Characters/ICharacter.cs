using Rnd.Api.Models.Effects;
using Rnd.Api.Models.Fields;
using Rnd.Api.Models.Parameters;
using Rnd.Api.Models.Resources;

namespace Rnd.Api.Models.Characters;

public interface ICharacter
{
    public Guid Id { get; }
    public Guid OwnerId { get; set; }
    
    public string Name { get; }
    public bool Locked { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public List<IField> Fields { get; }
    public List<IParameter> Parameters { get; }
    public List<IResource> Resources { get; }
    public List<IEffect> Effects { get; }
    
    public DateTime Created { get; }
    public DateTime? Edited { get; set; }
    public DateTime? LastPick { get; set; }
}