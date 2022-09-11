using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Logic.Models.Effects;
using Rnd.Api.Logic.Models.Fields;
using Rnd.Api.Logic.Models.Parameters;
using Rnd.Api.Logic.Models.Resources;

namespace Rnd.Api.Logic.Models.Characters;

public interface ICharacter : IStorable<Data.Entities.Character>
{
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