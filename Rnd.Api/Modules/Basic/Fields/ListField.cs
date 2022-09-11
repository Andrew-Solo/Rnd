namespace Rnd.Api.Modules.Basic.Fields;

public class ListField : Field<List<string>>
{
    public ListField(string path, string name, List<string>? ideals = null) : base(path, name)
    {
        Value = ideals ?? new List<string>();
    }

    public override FieldType Type => FieldType.List;
    public int MaxLength => 450;
}