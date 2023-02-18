using System.Globalization;

namespace Rnd.Bot.Discord.Views.Drawers;

public class NumberDrawer : Drawer<decimal?>
{
    public override string Draw(decimal? value)
    {
        return Convert.ToString(value, CultureInfo.InvariantCulture) ?? "—";
    }
}