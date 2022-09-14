using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Effects;
using Rnd.Api.Modules.Basic.Fields;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;

namespace Rnd.Api.Modules.Basic.Characters;

public interface ICharacter : IStorable<Data.Entities.Character>
{
    public Guid OwnerId { get; }
    
    public string Name { get; }
    public bool Locked { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public List<IField> Fields { get; }
    public List<IParameter> Parameters { get; }
    public List<IResource> Resources { get; }
    public List<IEffect> Effects { get; }
    
    public DateTimeOffset Created { get; }
    public DateTimeOffset? Edited { get; set; }
    public DateTimeOffset? LastPick { get; set; }
}