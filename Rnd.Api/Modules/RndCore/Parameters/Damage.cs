using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Damage : Int32Parameter
{
    public Damage(ICharacter character, int value) : base(character, nameof(Damage))
    {
        Value = value;
    }
    
    public override string Path => nameof(Leveling);
    public override bool Virtual => true;
}