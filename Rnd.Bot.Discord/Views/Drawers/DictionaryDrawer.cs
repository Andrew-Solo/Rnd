using System.Text;

namespace Rnd.Bot.Discord.Views.Drawers;

public class DictionaryDrawer : Drawer<IDictionary<string, object?>>
{
    public override string Draw(IDictionary<string, object?>? dictionary)
    {
        if (dictionary == null) return "—";

        var sb = new StringBuilder();

        sb.AppendLine("```md");
        
        foreach (var (key, value) in dictionary)
        {
            sb.AppendLine($"[{key}]({value})");
        }
        
        sb.AppendLine("```");

        return sb.ToString();
    }
}