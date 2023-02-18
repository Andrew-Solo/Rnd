using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class TextFieldBuilder : FieldBuilder
{
    public TextFieldBuilder(string name, string? value) : base(name)
    {
        _value = value;
    }
    
    public TextFieldBuilder WithDrawer(TextDrawer drawer)
    {
        Drawer = drawer;
        return this;
    }

    public new TextField Build()
    {
        return new TextField(Name, _value, Drawer as TextDrawer, IsInline);
    }
    
    private readonly string? _value;
}