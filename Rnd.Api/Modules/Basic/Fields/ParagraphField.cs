namespace Rnd.Api.Modules.Basic.Fields;

public class ParagraphField : TextField
{
    public ParagraphField(string path, string name, string? value = null) : base(path, name)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Paragraph;
    public override int MaxLength => 800;
}