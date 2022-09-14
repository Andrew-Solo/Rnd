using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class ShortField : TextField
{
    public ShortField(ICharacter character, string path, string name, string? value = null) : base(character, path, name)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Short;
    public override int MaxLength => 50;
}