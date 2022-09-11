namespace Rnd.Api.Modules.Basic.Fields;

public class MediumField : TextField
{
    public MediumField(string path, string name) : base(path, name) { }

    public override FieldType Type => FieldType.Medium;
    public override int MaxLength => 200;
}