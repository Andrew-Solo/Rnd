using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class TinyField : TextField
{
    public TinyField(ICharacter character, string path, string name) : base(character, path, name) { }

    public override FieldType Type => FieldType.Tiny;
    public override int MaxLength => 10;
}