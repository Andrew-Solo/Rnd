namespace Rnd.Bot.Discord.Views.Drawers;

public class InlineModifierDrawer : NumberDrawer
{
    public override string Draw(decimal? value)
    {
        if (value == null) return "—";

        var sign = value >= 0 ? "+" : "-";

        return $"`{sign}{value}`";
    }
}