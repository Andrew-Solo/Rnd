using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Damage : Int32Parameter
{
    public Damage(int value) : base(nameof(Damage))
    {
        Value = value;
    }
    
    public override string? Path => nameof(Leveling);
}