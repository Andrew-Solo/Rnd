using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Level : Int32Parameter
{
    public Level(int value) : base(nameof(Level))
    {
        Value = value;
    }
    
    public override string? Path => nameof(Leveling);
}