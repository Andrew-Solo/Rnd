using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources;

public class Power : StrictResource
{
    public Power(decimal value, decimal max) : base(nameof(Power))
    {
        Value = value;
        Max = max;
    }
    
    public override string Path => nameof(Leveling);
}