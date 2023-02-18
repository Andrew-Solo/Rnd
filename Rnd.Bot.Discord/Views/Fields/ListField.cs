using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class ListField : Field<List<string?>>
{
    public ListField(string name, List<string?>? value = null, bool isInline = false) 
        : base(new ListDrawer(), name, value, isInline)
    { }
}