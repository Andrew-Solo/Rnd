using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

//TODO nullable
public class NumberField : Field<decimal>
{
    public NumberField(ICharacter character, string path, string name, decimal? value = null) : base(character, path, name)
    {
        //Must can be null
        Value = value.GetValueOrDefault();
    }

    public override FieldType Type => FieldType.Number;
    public int MaxLength => 10;
}