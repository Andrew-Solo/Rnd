namespace Rnd.Api.Modules.Basic.Fields;

public class TinyField : TextField
{
    public TinyField(string path, string name) : base(path, name) { }

    public override FieldType Type => FieldType.Tiny;
    public override int MaxLength => 10;
}