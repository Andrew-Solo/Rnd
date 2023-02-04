using System.Text;
using System.Text.RegularExpressions;

namespace Rnd.Bot.Discord.Views.Drawers;

public class DictionaryDrawer : Drawer<IDictionary<string, dynamic?>>
{
    public DictionaryDrawer(bool drawValues = false)
    {
        DrawValues = drawValues;
    }

    public bool DrawValues { get; }

    public override string Draw(IDictionary<string, dynamic?>? dictionary)
    {
        if (dictionary == null) return "—";

        var sb = new StringBuilder();

        sb.AppendLine("```md");
        
        foreach (var (key, value) in dictionary)
        {
            sb.AppendLine($"[{key}]({DrawValue(value)})");
        }
        
        sb.AppendLine("```");

        return sb.ToString();
    }

    private string DrawValue(dynamic? value)
    {
        return DrawValues 
            ? Drawer.Draw(ViewData.ToJToken(value)) 
            : Regex.Replace($"{value}", "\\s+", " ");
    }
}