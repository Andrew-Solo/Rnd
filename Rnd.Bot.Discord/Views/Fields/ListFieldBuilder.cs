namespace Rnd.Bot.Discord.Views.Fields;

public class ListFieldBuilder : FieldBuilder
{
    public ListFieldBuilder(string name, List<string?>? value) : base(name)
    {
        _value = value;
    }
    
    public new ListField Build()
    {
        return new ListField(Name, _value, IsInline);
    }
    
    private readonly List<string?>? _value;
}