namespace Rnd.Api.Modules.Basic.Fields;

public class ShortField : TextField
{
    public ShortField(string path, string name) : base(path, name) { }

    public override FieldType Type => FieldType.Short;
    public override int MaxLength => 50;
}