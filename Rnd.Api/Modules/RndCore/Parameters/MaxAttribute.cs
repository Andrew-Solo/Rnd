using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class MaxAttribute : Int32Parameter
{
    public MaxAttribute(int value) : base(nameof(MaxAttribute))
    {
        Value = value;
    }
    
    public override string Path => nameof(Leveling);
}