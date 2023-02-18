namespace Rnd.Bot.Discord.Views.Drawers;

public class CounterDrawer : Drawer<(decimal, decimal)?>
{
    public override string Draw((decimal, decimal)? value)
    {
        
        return value == null ? "—" : $"```md\n<_{value.Value.Item1} / _{value.Value.Item2}>\n```";
    }
}