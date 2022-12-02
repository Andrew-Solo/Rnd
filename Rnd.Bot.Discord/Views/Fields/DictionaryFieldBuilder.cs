namespace Rnd.Bot.Discord.Views.Fields;

public class DictionaryFieldBuilder : FieldBuilder
{
    public DictionaryFieldBuilder(string name, IDictionary<string, object?>? value) : base(name)
    {
        _value = value;
    }
    
    public new DictionaryField Build()
    {
        return new DictionaryField(Name, _value, IsInline);
    }
    
    private readonly IDictionary<string, object?>? _value;
}