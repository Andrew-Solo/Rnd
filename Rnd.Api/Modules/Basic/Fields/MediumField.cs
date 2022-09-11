namespace Rnd.Api.Modules.Basic.Fields;

public class MediumField : TextField
{
    public MediumField(string path, string name, string? value = null) : base(path, name)
    {
        Value = value;
    }

    public override FieldType Type => FieldType.Medium;
    public override int MaxLength => 200;
}