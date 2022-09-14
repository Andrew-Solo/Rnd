using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources;

public class Drama : StrictResource
{
    public Drama(IEntity entity) : base(entity) { }
    
    public Drama(ICharacter character, int value = 0) : base(character, nameof(Drama))
    {
        Min = -3;
        Max = 3;
        Value = value;
    }

    public override string Path => nameof(Leveling);
}