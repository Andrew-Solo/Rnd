using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class NumberField : Field<decimal?>
{
    public NumberField(string name, decimal? value = default, NumberDrawer? drawer = null, bool isInline = false) 
        : base(drawer ?? new NumberDrawer(), name, value, isInline)
    { }
}