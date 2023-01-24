using System.Text;

namespace Rnd.Bot.Discord.Views.Drawers;

public class DictionaryDrawer : Drawer<IDictionary<string, dynamic>>
{
    public override string Draw(IDictionary<string, dynamic>? dictionary)
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