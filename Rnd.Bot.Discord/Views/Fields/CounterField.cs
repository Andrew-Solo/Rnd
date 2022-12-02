using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class CounterField : Field<(decimal, decimal)?>
{
    public CounterField(string name, (decimal, decimal)? value = default, bool isInline = false) 
        : base(new CounterDrawer(), name, value, isInline)
    { }
}