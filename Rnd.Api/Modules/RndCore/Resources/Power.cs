using Rnd.Api.Modules.Basic.Resources;

namespace Rnd.Api.Modules.RndCore.Resources;

public class Power : StrictResource
{
    public Power(decimal value, decimal max) : base(nameof(Power))
    {
        Value = value;
        Max = max;
    }
}