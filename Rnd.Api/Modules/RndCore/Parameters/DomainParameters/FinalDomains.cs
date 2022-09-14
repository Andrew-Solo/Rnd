using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class FinalDomains : Domains
{
    public FinalDomains(Character character) : base(character)
    {
        Character = character;
    }
    
    public Character Character { get; }

    public override Domain War => Character.Effects
        .Aggregate(new FinalDomain(Character, base.War), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Mist => Character.Effects
        .Aggregate(new FinalDomain(Character, base.Mist), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Way => Character.Effects
        .Aggregate(new FinalDomain(Character, base.Way), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Word => Character.Effects
        .Aggregate(new FinalDomain(Character, base.Word), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Lore => Character.Effects
        .Aggregate(new FinalDomain(Character, base.Lore), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Work => Character.Effects
        .Aggregate(new FinalDomain(Character, base.Work), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Art => Character.Effects
        .Aggregate(new FinalDomain(Character, base.Art), (attribute, effect) => effect.ModifyParameter(attribute));
}