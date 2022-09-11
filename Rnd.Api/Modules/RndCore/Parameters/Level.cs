using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Level : Int32Parameter
{
    public Level(int value) : base(nameof(Level))
    {
        Value = value;
    }
}