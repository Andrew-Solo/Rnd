using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class MaxAttribute : Int32Parameter
{
    public MaxAttribute(ICharacter character, int value) : base(character, nameof(MaxAttribute))
    {
        Value = value;
    }
    
    public override string Path => nameof(Leveling);
    public override bool Virtual => true;
}