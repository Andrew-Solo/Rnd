using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public class ListField : Field<List<string>>
{
    public ListField(ICharacter character, string path, string name, List<string>? value = null) 
        : base(character, path, name)
    {
        Value = value ?? new List<string>();
    }

    public override FieldType Type => FieldType.List;
    public int MaxLength => 450;
}