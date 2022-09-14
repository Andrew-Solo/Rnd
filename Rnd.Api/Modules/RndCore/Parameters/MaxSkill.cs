using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class MaxSkill : Int32Parameter
{
    public MaxSkill(ICharacter character, int value) : base(character, nameof(MaxSkill))
    {
        Value = value;
    }
    
    public override string Path => nameof(Leveling);
}