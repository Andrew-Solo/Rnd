namespace Rnd.Bot.Discord.Views.Drawers;

public class ListDrawer : Drawer<List<string?>>
{
    public ListDrawer(bool inline = false)
    {
        Inline = inline;
    }

    public bool Inline { get; }

    public override string Draw(List<string?>? value)
    {
        return value == null 
            ? "—" 
            : Inline 
                ? String.Join(", ", value.Where(s => s != null))
                : "— " + String.Join("\n— ", value.Where(s => s != null));
    }
}