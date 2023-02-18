using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class NumberFieldBuilder : FieldBuilder
{
    public NumberFieldBuilder(string name, decimal? value) : base(name)
    {
        _value = value;
    }
    
    public NumberFieldBuilder WithDrawer(NumberDrawer drawer)
    {
        Drawer = drawer;
        return this;
    }

    public new NumberField Build()
    {
        return new NumberField(Name, _value, Drawer as NumberDrawer, IsInline);
    }
    
    private readonly decimal? _value;
}