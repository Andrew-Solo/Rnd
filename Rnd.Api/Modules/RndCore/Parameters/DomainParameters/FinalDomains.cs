using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class FinalDomains : Domains
{
    public FinalDomains(ICharacter character) : base(character) { }

    public override Domain War => GetDomain(DomainType.War, true);
    public override Domain Mist => GetDomain(DomainType.Mist, true);
    public override Domain Way => GetDomain(DomainType.Way, true);
    public override Domain Word => GetDomain(DomainType.Word, true);
    public override Domain Lore => GetDomain(DomainType.Lore, true);
    public override Domain Work => GetDomain(DomainType.Work, true);
    public override Domain Art => GetDomain(DomainType.Art, true);
}