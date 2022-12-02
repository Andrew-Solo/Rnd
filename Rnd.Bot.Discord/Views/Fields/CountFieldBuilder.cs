namespace Rnd.Bot.Discord.Views.Fields;

public class CountFieldBuilder : FieldBuilder
{
    public CountFieldBuilder(string name, (decimal, decimal)? value) : base(name)
    {
        _value = value;
    }
    
    public new CounterField Build()
    {
        return new CounterField(Name, _value, IsInline);
    }
    
    private readonly (decimal, decimal)? _value;
}