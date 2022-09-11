namespace Rnd.Api.Modules.Basic.Fields;

public class ListField : Field<List<string>>
{
    public ListField(string path, string name) : base(path, name) { }

    public override FieldType Type => FieldType.List;
}