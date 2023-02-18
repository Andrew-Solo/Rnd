using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class TextField : Field<string?>
{
    public TextField(string name, string? value = null, TextDrawer? drawer = null, bool isInline = false) 
        : base(drawer ?? new TextDrawer(), name, value, isInline)
    { }
}