namespace Rnd.Bot.Discord.Views.Drawers;

public class TimeSpanDrawer : Drawer<TimeSpan?>
{
    public override string Draw(TimeSpan? value)
    {
        return value?.ToString() ?? "—";
    }
}