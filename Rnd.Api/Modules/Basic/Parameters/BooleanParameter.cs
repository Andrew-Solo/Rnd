using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Parameters;

public class BooleanParameter : Parameter<Boolean>
{
    public BooleanParameter(IEntity entity) : base(entity) { }
    public BooleanParameter(ICharacter character, string name) : base(character, name) { }
}