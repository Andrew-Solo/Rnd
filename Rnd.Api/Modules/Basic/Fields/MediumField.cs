using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class MediumField : TextField
{
    public MediumField(ICharacter character, string path, string name, string? value = null) : base(character, path, name)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Medium;
    public override int MaxLength => 200;
}