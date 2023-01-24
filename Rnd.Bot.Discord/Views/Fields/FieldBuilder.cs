using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class FieldBuilder
{
    protected FieldBuilder(string name)
    {
        Name = name;
    }
    
    public static FieldBuilder WithName(string name)
    {
        return new FieldBuilder(name);
    }

    public TextFieldBuilder WithValue(string? value)
    {
        return new TextFieldBuilder(Name, value);
    }
    
    public ListFieldBuilder WithValue(IEnumerable<string?>? values)
    {
        return WithValue(values?.ToArray());
    }
    
    public ListFieldBuilder WithValue(params string?[]? values)
    {
        return new ListFieldBuilder(Name, values?.ToList());
    }
    
    public NumberFieldBuilder WithValue(int? value)
    {
        return WithValue((decimal?) value);
    }
    
    public NumberFieldBuilder WithValue(decimal? value)
    {
        return new NumberFieldBuilder(Name, value);
    }
    
    public DictionaryFieldBuilder WithValue(IDictionary<string, string?>? value)
    {
        IDictionary<string, object?>? dictionary = null;
        if (value != null) dictionary = value.ToDictionary(pair => pair.Key, pair => (object?) pair.Value);
        return WithValue(dictionary);
    }
    
    public DictionaryFieldBuilder WithValue(IDictionary<string, int?>? value)
    {
        IDictionary<string, object?>? dictionary = null;
        if (value != null) dictionary = value.ToDictionary(pair => pair.Key, pair => (object?) pair.Value);
        return WithValue(dictionary);
    }
    
    public DictionaryFieldBuilder WithValue(IDictionary<string, decimal?>? value)
    {
        IDictionary<string, object?>? dictionary = null;
        if (value != null) dictionary = value.ToDictionary(pair => pair.Key, pair => (object?) pair.Value);
        return WithValue(dictionary);
    }
    
    public DictionaryFieldBuilder WithValue(IDictionary<string, dynamic>? value)
    {
        return new DictionaryFieldBuilder(Name, value);
    }
    
    public CountFieldBuilder WithValue((int, int)? value)
    {
        (decimal, decimal)? tuple = null;
        if (value != null) tuple = (value.Value.Item1, value.Value.Item2);
        return WithValue(tuple);
    }
    
    public CountFieldBuilder WithValue((decimal, decimal)? value)
    {
        return new CountFieldBuilder(Name, value);
    }
    
    public FieldBuilder Inline(bool isInline = true)
    {
        IsInline = isInline;
        return this;
    }
    
    public FieldBuilder NotInline()
    {
        IsInline = false;
        return this;
    }

    public virtual IField Build()
    {
        return new TextField(Name, null, new TextDrawer(), IsInline);
    }

    protected string Name { get; }
    protected IDrawer? Drawer { get; set; }
    protected bool IsInline { get; set; }
}