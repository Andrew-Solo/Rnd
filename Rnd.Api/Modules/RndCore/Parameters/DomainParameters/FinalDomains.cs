using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class FinalDomains : Domains
{
    public FinalDomains(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }

    
}