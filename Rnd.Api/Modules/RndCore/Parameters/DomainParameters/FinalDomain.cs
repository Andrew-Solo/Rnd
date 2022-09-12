using Rnd.Api.Helpers;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class FinalDomain : Domain
{
    public FinalDomain(Domain original) : base(original.DomainType, original.Value)
    {
        
    }

    public override string Path => PathHelper.Combine(nameof(Final), base.Path);
}