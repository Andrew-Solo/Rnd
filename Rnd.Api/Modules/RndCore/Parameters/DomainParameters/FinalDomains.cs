using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class FinalDomains : Domains
{
    public FinalDomains(ICharacter character) : base(character) { }

    public override Domain War => Character.Effects
        .Aggregate(GetDomain(DomainType.War, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Mist => Character.Effects
        .Aggregate(GetDomain(DomainType.Mist, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Way => Character.Effects
        .Aggregate(GetDomain(DomainType.Way, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Word => Character.Effects
        .Aggregate(GetDomain(DomainType.Word, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Lore => Character.Effects
        .Aggregate(GetDomain(DomainType.Lore, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Work => Character.Effects
        .Aggregate(GetDomain(DomainType.Work, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Domain Art => Character.Effects
        .Aggregate(GetDomain(DomainType.Art, true), (attribute, effect) => effect.ModifyParameter(attribute));
}