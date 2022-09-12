namespace Rnd.Api.Modules.Basic.Fields;

//TODO nullable
public class NumberField : Field<decimal>
{
    public NumberField(string path, string name, decimal? value = null) : base(path, name)
    {
        //Must can be null
        Value = value.GetValueOrDefault();
    }

    public override FieldType Type => FieldType.Number;
    public int MaxLength => 10;
}