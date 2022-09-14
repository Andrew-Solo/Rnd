using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Parameters;

public class DecimalParameter : Parameter<Decimal>
{
    public DecimalParameter(IEntity entity) : base(entity) { }
    public DecimalParameter(ICharacter character, string name) : base(character, name) { }
}