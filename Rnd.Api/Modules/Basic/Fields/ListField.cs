using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class ListField : Field<List<string>>
{
    public ListField(IEntity entity) : base(entity) { }
    public ListField(ICharacter character, string name, List<string>? value = null, string? path = null) 
        : base(character, name, path)
    {
        Value = value ?? new List<string>();
    }

    public override FieldType Type => FieldType.List;
    public int MaxLength => 450;
}