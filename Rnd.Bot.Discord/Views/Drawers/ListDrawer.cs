namespace Rnd.Bot.Discord.Views.Drawers;

public class ListDrawer : Drawer<List<string?>>
{
    public override string Draw(List<string?>? value)
    {
        return value == null ? "—" : "— " + String.Join("\n— ", value.Where(s => s != null));
    }
}