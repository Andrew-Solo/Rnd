namespace Rnd.Api.Modules.Basic.Fields;

public class ShortField : TextField
{
    public ShortField(string path, string name, string? value = null) : base(path, name)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Short;
    public override int MaxLength => 50;
}