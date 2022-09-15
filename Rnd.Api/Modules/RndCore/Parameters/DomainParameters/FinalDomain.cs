using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class FinalDomain : Domain
{
    public FinalDomain(ICharacter character, Domain original) : base(character, original.DomainType, original.Value)
    {
        
    }

    public override string Path => Helpers.Path.Combine(nameof(Final), base.Path);
    public override bool Virtual => true;
}