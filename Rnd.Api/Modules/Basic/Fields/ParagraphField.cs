using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class ParagraphField : TextField
{
    public ParagraphField(ICharacter character, string name, string? value = null, string? path = null) 
        : base(character, name, path)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Paragraph;
    public override int MaxLength => 800;
}