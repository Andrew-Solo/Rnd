using Rnd.Bot.Discord.Views.Drawers;

namespace Rnd.Bot.Discord.Views.Fields;

public class DictionaryField : Field<IDictionary<string, dynamic?>>
{
    public DictionaryField(string name, IDictionary<string, dynamic?>? value = null, bool isInline = false) 
        : base(new DictionaryDrawer(), name, value, isInline)
    { }
}