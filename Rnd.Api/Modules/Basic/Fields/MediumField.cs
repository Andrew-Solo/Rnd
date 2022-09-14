using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class MediumField : TextField
{
    public MediumField(ICharacter character, string name, string? value = null, string? path = null) 
        : base(character, name, path)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Medium;
    public override int MaxLength => 200;
}