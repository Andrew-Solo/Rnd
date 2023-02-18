namespace Rnd.Bot.Discord.Views.Drawers;

public class ModifierDrawer : NumberDrawer
{
    public override string Draw(decimal? value)
    {
        if (value == null) return "—";

        var sign = value >= 0 ? "+" : "-";

        return $"```md\n# {sign}{value}\n```";
    }
}