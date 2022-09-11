namespace Rnd.Api.Modules.Basic.Fields;

public class ParagraphField : TextField
{
    public ParagraphField(string path, string name) : base(path, name) { }

    public override FieldType Type => FieldType.Paragraph;
    public override int MaxLength => 800;
}