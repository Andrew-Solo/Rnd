using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Damage : Int32Parameter
{
    public Damage(int value) : base(nameof(Damage))
    {
        Value = value;
    }
}