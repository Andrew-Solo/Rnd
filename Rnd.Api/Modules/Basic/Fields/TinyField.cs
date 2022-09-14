using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class TinyField : TextField
{
    public TinyField(IEntity entity) : base(entity) { }
    public TinyField(ICharacter character, string name, string? path = null) : base(character, name, path) { }

    public override FieldType Type => FieldType.Tiny;
    public override int MaxLength => 10;
}