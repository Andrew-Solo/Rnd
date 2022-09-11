namespace Rnd.Api.Modules.Basic.Fields;

public class NumberField : Field<decimal>
{
    public NumberField(string path, string name) : base(path, name) { }

    public override FieldType Type => FieldType.Number;
}