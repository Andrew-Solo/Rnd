using System.Globalization;

namespace Rnd.Bot.Discord.Views.Drawers;

public class DateTimeOffsetDrawer : Drawer<DateTimeOffset?>
{
    public override string Draw(DateTimeOffset? value)
    {
        return value?.LocalDateTime.ToString(CultureInfo.CurrentCulture) ?? "—";
    }
}