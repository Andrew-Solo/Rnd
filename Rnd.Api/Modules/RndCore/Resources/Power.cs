using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources;

public class Power : StrictResource
{
    public Power(ICharacter character, decimal value, decimal max) : base(character, nameof(Power))
    {
        Value = value;
        Max = max;
    }
    
    public override string Path => nameof(Leveling);
}