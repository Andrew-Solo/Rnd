using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Parameters;

public class Int32Parameter : Parameter<Int32>
{
    public Int32Parameter(IEntity entity) : base(entity) { }
    public Int32Parameter(ICharacter character, string name) : base(character, name) { }
}