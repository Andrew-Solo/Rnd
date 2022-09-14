using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

//TODO nullable
public class NumberField : Field<decimal>
{
    public NumberField(IEntity entity) : base(entity) { }
    public NumberField(ICharacter character, string name, decimal? value = null, string? path = null) 
        : base(character, name, path)
    {
        //Must can be null
        Value = value.GetValueOrDefault();
    }

    public override FieldType Type => FieldType.Number;
    public int MaxLength => 10;
}