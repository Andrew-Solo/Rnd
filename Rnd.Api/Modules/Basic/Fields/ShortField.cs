using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class ShortField : TextField
{
    public ShortField(IEntity entity) : base(entity) { }
    public ShortField(ICharacter character, string name, string? value = null, string? path = null) 
        : base(character, name, path)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Short;
    public override int MaxLength => 50;
}