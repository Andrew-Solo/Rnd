using Discord;
using Rnd.Bot.Discord.Views.Drawers;
using Rnd.Bot.Discord.Views.Panels;

namespace Rnd.Bot.Discord.Views.Fields;

public abstract class Field<T> : IField
{
    protected Field(Drawer<T> drawer, string name, T? value = default, bool isInline = false)
    {
        Drawer = drawer;
        Name = name;
        Value = value;
        IsInline = isInline;
    }

    public virtual string Name { get; }
    public virtual T? Value { get; }
    public virtual bool IsInline { get; }

    public Drawer<T> Drawer { get; }

    public string DrawValue() => Drawer.Draw(Value);
    public SinglePanel AsPanel() => new(this);
    public Embed AsEmbed() => AsPanel().AsEmbed();
    
    public EmbedFieldBuilder AsEmbedField()
    {
        return new EmbedFieldBuilder
        {
            Name = Name,
            Value = DrawValue(),
            IsInline = IsInline
        };
    }

    IDrawer IField.Drawer => Drawer;
    object? IField.Value => Value;
}